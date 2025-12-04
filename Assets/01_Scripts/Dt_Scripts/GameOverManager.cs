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

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (scene.Contains("Level"))  // usa tus nombres reales
        {
            GlobalAudioController gc = FindFirstObjectByType<GlobalAudioController>();
            if (gc != null)
                gc.MuteAllExcept("GameOverVol");
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            Animator anim = gameOverPanel.GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Show");
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

        GlobalAudioController gc = FindFirstObjectByType<GlobalAudioController>();
        if (gc != null)
            gc.RestoreAll();

        SceneManager.LoadScene(sceneName);
    }
}