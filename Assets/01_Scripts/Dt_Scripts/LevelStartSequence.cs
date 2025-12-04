using UnityEngine;
using System.Collections;

public class LevelStartSequence : MonoBehaviour
{
    [Header("Freeze / Audio")]
    public float freezeDuration = 2f;
    public AudioSource introAudio;
    public bool waitForAudioToEnd = true;

    [Header("Camera Zoom")]
    public Camera targetCamera; // normalmente Camera.main
    public float targetOrthographicSize = 8f; // zoom out size
    public float zoomSpeed = 2f; // velocidad del zoom (mayor = más rápido)

    private float originalSize;

    void Start()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null)
        {
            Debug.LogWarning("LevelStartSequence: No camera encontrada.");
            StartCoroutine(EndIfNoCamera());
            return;
        }

        originalSize = targetCamera.orthographicSize;
        StartCoroutine(Sequence());
    }

    private IEnumerator EndIfNoCamera()
    {
        yield return null;
    }

    private IEnumerator Sequence()
    {
        // 1) Pausar todo
        Time.timeScale = 0f;

        // 2) Reproducir audio si existe
        if (introAudio != null)
        {
            introAudio.Play();
        }

        // 3) Hacer zoom OUT (en tiempo real)
        float t = 0f;
        float startSize = originalSize;
        float endSize = targetOrthographicSize;

        while (Mathf.Abs(targetCamera.orthographicSize - endSize) > 0.01f)
        {
            // avanzar en tiempo real
            t += Time.unscaledDeltaTime * zoomSpeed;
            targetCamera.orthographicSize = Mathf.Lerp(startSize, endSize, Mathf.Clamp01(t));
            yield return null;
        }

        // 4) Esperar: si waitForAudioToEnd y hay audio -> esperar hasta que termine o hasta freezeDuration, lo que sea mayor.
        float waitTime = freezeDuration;
        if (introAudio != null && introAudio.clip != null && waitForAudioToEnd)
            waitTime = Mathf.Max(freezeDuration, introAudio.clip.length);

        yield return new WaitForSecondsRealtime(waitTime);

        // 5) Zoom IN (volver a tamaño original)
        t = 0f;
        startSize = targetCamera.orthographicSize;
        endSize = originalSize;
        while (Mathf.Abs(targetCamera.orthographicSize - endSize) > 0.01f)
        {
            t += Time.unscaledDeltaTime * zoomSpeed;
            targetCamera.orthographicSize = Mathf.Lerp(startSize, endSize, Mathf.Clamp01(t));
            yield return null;
        }

        // 6) Restaurar tiempo
        Time.timeScale = 1f;
    }
}