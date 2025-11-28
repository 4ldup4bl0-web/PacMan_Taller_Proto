using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Prefab del enemigo
    public Transform respawnPoint;       // Punto donde aparece
    public float respawnTime = 10f;

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        Debug.Log("Esperando respawn...");
        yield return new WaitForSeconds(respawnTime);

        GameObject newEnemy =
            Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);

        // Le asigno el spawner al nuevo enemigo
        newEnemy.GetComponent<EnemyElimination>().spawner = this;

        Debug.Log("Enemigo respawneado correctamente.");
    }
}
