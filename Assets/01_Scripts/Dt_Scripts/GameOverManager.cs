using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject gameOverPanel; // panel con UI del game over

    public AudioSource gameOverAudio;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (gameOverAudio != null)
            gameOverAudio.Play();
    
        else
        {
            Debug.LogWarning("GameOverManager: No hay panel asignado.");
        }

        // Opcional: desactivar tiempo
        Time.timeScale = 0f;
    }


    public void GoToMainMenu(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}