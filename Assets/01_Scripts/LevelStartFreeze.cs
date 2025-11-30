using UnityEngine;

public class LevelStartFreeze : MonoBehaviour
{
    [Header("Tiempo de congelamiento")]
    public float freezeDuration = 2f;

    [Header("Audio de inicio de nivel")]
    public AudioSource startLevelAudio;

    void Start()
    {
        StartCoroutine(FreezeSequence());
    }

    private System.Collections.IEnumerator FreezeSequence()
    {
        // Pausar todo
        Time.timeScale = 0f;

        // Reproducir música si existe
        if (startLevelAudio != null)
            startLevelAudio.Play();

        // Esperar lo que dure la música o el freezeDuration, lo que sea más largo
        float waitTime = freezeDuration;

        if (startLevelAudio != null && startLevelAudio.clip != null)
        {
            waitTime = Mathf.Max(freezeDuration, startLevelAudio.clip.length);
        }

        yield return new WaitForSecondsRealtime(waitTime);

        // Volver a la normalidad
        Time.timeScale = 1f;
    }
}