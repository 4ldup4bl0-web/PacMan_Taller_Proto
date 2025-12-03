using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class GlobalAudioController : MonoBehaviour
{
    public AudioMixer mixer;

    [Header("Restauración automática")]
    public float restoreDelay = 3f;  // Tiempo antes de restaurar

    private Coroutine restoreRoutine;

    public void MuteAllExcept(string exposedParam)
    {
        // Silenciar TODO
        mixer.SetFloat("BGMVolume", -80f);
        mixer.SetFloat("SFXVolume", -80f);

        // Activar solo el canal permitido
        mixer.SetFloat(exposedParam, 0f);

        // Iniciar restauración automática
        if (restoreRoutine != null)
            StopCoroutine(restoreRoutine);

        restoreRoutine = StartCoroutine(RestoreAfterDelay());
    }

    private IEnumerator RestoreAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restoreDelay);

        RestoreAll();
    }

    public void RestoreAll()
    {
        // Restaurar volúmenes normales
        mixer.SetFloat("BGMVolume", 0f);
        mixer.SetFloat("SFXVolume", 0f);

        // Silenciar los que no deben sonar por defecto
        mixer.SetFloat("GameOverVol", -80f);
        mixer.SetFloat("VictoryVol", -80f);
    }
}