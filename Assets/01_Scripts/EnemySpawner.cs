using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform respawnPoint;
    public float respawnTime = 10f;

    private bool enemyAlive = false;

    private void Start()
    {
        // FIX: si ya existe un enemigo en escena NO spawnear otro
        EnemyElimination existing = FindObjectOfType<EnemyElimination>();

        if (existing != null)
        {
            enemyAlive = true;
            existing.spawner = this; // Asegura vínculo correcto
            return; // ← NO spawneamos extra
        }

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (enemyAlive) return;

        GameObject newEnemy =
            Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);

        newEnemy.GetComponent<EnemyElimination>().spawner = this;
        enemyAlive = true;

        Debug.Log("Enemigo creado.");
    }

    public void OnEnemyDeath()
    {
        enemyAlive = false;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        Debug.Log("Esperando respawn...");
        yield return new WaitForSeconds(respawnTime);

        SpawnEnemy();
        Debug.Log("Enemigo respawneado correctamente.");
    }
}
