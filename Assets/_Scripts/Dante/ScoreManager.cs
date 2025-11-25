using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    // 1. VARIABLE DE PUNTUACIÓN CENTRAL
    private int score = 0;

    // Asigna tu objeto Text o TextMeshPro a esta variable en el Inspector de Unity
    public TextMeshProUGUI scoreText;

    // Variable estática para acceder al gestor desde cualquier otro script
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        // Asegura que solo exista una instancia de este Manager en la escena
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Esto evita que el gestor se destruya al cambiar de escena
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializar la UI al comenzar
        UpdateScoreUI();
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("Puntos obtenidos: " + pointsToAdd + ". Puntuación total: " + score);
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            // Actualiza el texto de la UI. Puedes personalizar el formato aquí.
            scoreText.text = "PUNTOS: " + score.ToString();
        }
    }
}