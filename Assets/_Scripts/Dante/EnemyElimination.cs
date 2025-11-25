using UnityEngine;
using UnityEngine.Playables;

public class EnemyElimination : MonoBehaviour
{
    public int pointsValue;

    private Chainsaw playerState;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerState = player.GetComponent<Chainsaw>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Verificar si colisionó con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. Verificar si el jugador tiene el Power-Up
            if (playerState != null && playerState.hasPowerUp)
            {
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddPoints(pointsValue);
                }

                // Puedes cambiar esto por un efecto de "reaparecer en base" o animación de fantasma
                Destroy(gameObject);
            }
            else
            {
                // Aquí iría la lógica de daño al jugador
                Debug.Log("El jugador ha sido golpeado por el enemigo.");
            }
        }
    }
}