using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[DisallowMultipleComponent]



public class OptionsMenuu : MonoBehaviour
{


    [SerializeField] private GameObject optionsPanel;


    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer mixer;

    private void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        MainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        mixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
    }
    private void Update()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
        mixer.SetFloat("BGMVolume", Mathf.Log10(BGMSlider.value) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);

    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void MainMenu()
    {
        optionsPanel.SetActive(false);
    }

}