using UnityEngine;

public class PointGiver : MonoBehaviour
{
    public int pointsValue; // Valor de cada punto

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(pointsValue);
            }

            Destroy(gameObject);
        }
    }
}