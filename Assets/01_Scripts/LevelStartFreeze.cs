using UnityEngine;

public class LevelStartFreeze : MonoBehaviour
{
    [Header("Tiempo de congelamiento mínimo")]
    public float freezeDuration = 2f;

    [Header("Audio")]
    public AudioSource introMusic;     // Música de inicio
    public AudioSource backgroundMusic; // Música de fondo

    void Start()
    {
        StartCoroutine(FreezeSequence());
    }

    private System.Collections.IEnumerator FreezeSequence()
    {
        // Pausar todo
        Time.timeScale = 0f;

        float waitTime = freezeDuration;

        // Reproducir música de intro si existe
        if (introMusic != null && introMusic.clip != null)
        {
            introMusic.Play();

            // Esperar lo que dure la música o freezeDuration (lo que sea mayor)
            waitTime = Mathf.Max(freezeDuration, introMusic.clip.length);
        }

        // Espera con Time.timeScale = 0
        yield return new WaitForSecondsRealtime(waitTime);

        // Volver al tiempo normal
        Time.timeScale = 1f;

        // Iniciar música de fondo después de la intro
        if (backgroundMusic != null)
            backgroundMusic.Play();
    }
}