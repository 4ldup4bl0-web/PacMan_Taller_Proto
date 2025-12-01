using UnityEngine;
using TMPro;

public class LevelGoalManager : MonoBehaviour
{
    [Header("Condiciones del nivel")]
    public int requiredPoints = 200;
    public float levelTimeLimit = 30f;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI goalText;

    private float timer;
    private bool levelEnded = false;

    private LevelTransitionManager transition;

    void Start()
    {
        timer = levelTimeLimit;

        // Mostrar puntaje objetivo en pantalla
        if (goalText != null)
            goalText.text = "Meta: " + requiredPoints + " pts";

        // Buscar transition manager
        transition = FindFirstObjectByType<LevelTransitionManager>();
        if (transition == null)
        {
            Debug.LogError(" No se encontró LevelTransitionManager en la escena.");
        }
    }

    void Update()
    {
        if (levelEnded) return;

        //------------------------
        //  ACTUALIZAR TEMPORIZADOR
        //------------------------
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;

        // Actualizar texto del tiempo
        if (timeText != null)
            timeText.text = "Tiempo: " + timer.ToString("F1");

        //------------------------
        //  COMPLETÓ META DE PUNTOS
        //------------------------
        if (ScoreManager.Instance.GetScore() >= requiredPoints)
        {
            levelEnded = true;
            GoToNextLevel();
            return;
        }

        //------------------------
        // SE ACABÓ EL TIEMPO GAME OVER
        //------------------------
        if (timer <= 0)
        {
            levelEnded = true;

            if (GameOverManager.Instance != null)
                GameOverManager.Instance.ShowGameOver();
            else
                Debug.LogError(" Falta GameOverManager en la escena.");
        }
    }

    void GoToNextLevel()
    {
        if (transition != null)
        {
            Debug.Log(" Cambiando de nivel usando TRANSITION MANAGER...");
            StartCoroutine(transition.LevelEndSequence());
        }
        else
        {
            Debug.LogError("No existe LevelTransitionManager en la escena.");
        }
    }
}