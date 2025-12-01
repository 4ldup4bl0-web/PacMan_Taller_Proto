using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject Mainmenu;

    private void Start()
    {
        Mainmenu.SetActive(true);
        OptionsMenu.SetActive(false);

    }

    public void OpenOptionsPanel()
    {
        Mainmenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void OpenMainMenuPanel()
    {
        Mainmenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Pacman");
    }
}
