using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    public string[] levelScenes; // SOLO niveles
    public int targetScore = 50;
    public float waitTime = 3f;
    public AudioSource levelCompleteAudio;

    private bool levelEnding = false;

    void Update()
    {
        if (!levelEnding && ScoreManager.Instance.GetScore() >= targetScore)
        {
            StartCoroutine(LevelEndSequence());
        }
    }

    public System.Collections.IEnumerator LevelEndSequence()
    {
        levelEnding = true;
        Time.timeScale = 0f;

        if (levelCompleteAudio != null)
            levelCompleteAudio.Play();

        yield return new WaitForSecondsRealtime(waitTime);

        Time.timeScale = 1f;

        // Obtener la escena actual
        string currentScene = SceneManager.GetActiveScene().name;

        // Buscar la siguiente en la lista
        for (int i = 0; i < levelScenes.Length; i++)
        {
            if (levelScenes[i] == currentScene)
            {
                int nextIndex = i + 1;

                if (nextIndex < levelScenes.Length)
                {
                    SceneManager.LoadScene(levelScenes[nextIndex]);
                }
                else
                {
                    Debug.Log("NO HAY MÁS NIVELES.");
                }
                yield break;
            }
        }
    }
}