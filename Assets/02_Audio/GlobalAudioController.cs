using UnityEngine;
using UnityEngine.Audio;

public class GlobalAudioController : MonoBehaviour
{
    public AudioMixer mixer;

    public void MuteAllExcept(string exposedParam)
    {

        mixer.SetFloat("BGMVolume", -80f);
        mixer.SetFloat("SFXVolume", -80f);


        mixer.SetFloat(exposedParam, 0f);


    }
}