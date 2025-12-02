using UnityEngine;

public class EnemyElimination : MonoBehaviour
{
    public int pointsValue = 100;
    public EnemySpawner spawner;
    private Chainsaw playerState;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerState = player.GetComponent<Chainsaw>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (playerState == null)
            return;

        if (playerState.hasPowerUp)
        {
            Debug.Log("Enemigo destruido.");
            ScoreManager.Instance.AddPoints(pointsValue);

            spawner.OnEnemyDeath(); // ← AVISA AL SPAWNER
            if (SFXManager.Instance != null) SFXManager.Instance.PlayEnemyEliminated();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Jugador golpeado.");
        }
    }
}
