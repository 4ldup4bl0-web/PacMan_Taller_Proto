using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    [Header("Player")]
    public GameObject player; // Si se deja vac�o, buscar� por tag "Player"
    public float respawnDelay = 2f;

    [Header("Spawn Points")]
    public Transform[] playerRespawnPoints;
    public Transform[] enemySpawnPoints;
    public float minEnemyDistanceFromPlayer = 6f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p;
                Debug.Log("[RespawnManager] Player encontrado por tag.");
            }
            else
            {
                Debug.LogWarning("[RespawnManager] No est� asignado 'player' y no se encontr� un GameObject con tag 'Player'. Asigna manualmente en el inspector.");
            }
        }
    }

    public void HandlePlayerHit()
    {
        Debug.Log("[RespawnManager] HandlePlayerHit llamado.");
        StartCoroutine(RespawnRoutine());
    }

    public void ForceRespawnPlayer()
    {
        Debug.Log("[RespawnManager] ForceRespawnPlayer llamado.");
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (player == null)
        {
            Debug.LogError("[RespawnManager] No hay jugador asignado. Abortando respawn.");
            yield break;
        }

        Debug.Log("[RespawnManager] Iniciando secuencia de respawn. Desactivando controles y colisiones.");

        // Intenta desactivar componentes relevantes del jugador
        var rb = player.GetComponent<Rigidbody2D>();
        var coll = player.GetComponent<Collider2D>();
        var movement = player.GetComponent<MonoBehaviour>(); // intento gen�rico; si tienes un script Movement, c�mbialo

        // Si tienes un script llamado Movement (recomendado), intenta obtenerlo espec�ficamente:
        var movementSpecific = player.GetComponent("Movement") as MonoBehaviour;
        if (movementSpecific != null) movement = movementSpecific;

        if (movement != null) movement.enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false; // evita interacciones f�sicas mientras est� "muerto"
        }
        if (coll != null) coll.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Elegir spawn m�s cercano (si hay)
        Transform chosen = FindNearestSpawn(player.transform.position, playerRespawnPoints);
        if (chosen != null)
        {
            player.transform.position = chosen.position;
            Debug.Log("[RespawnManager] Jugador reposicionado en spawn: " + chosen.name);
        }
        else
        {
            Debug.LogWarning("[RespawnManager] No hay playerRespawnPoints asignados. Jugador no se movi�.");
        }

        // Reubicar enemigos vivos a puntos lejos del jugador
        RepositionEnemiesAwayFromPlayer();

        // Reactivar
        if (coll != null) coll.enabled = true;
        if (rb != null) rb.simulated = true;
        if (movement != null) movement.enabled = true;

        // Asegurar velocidades 0
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("[RespawnManager] Respawn completado y jugador re-activado.");
    }

    private Transform FindNearestSpawn(Vector3 pos, Transform[] spawns)
    {
        if (spawns == null || spawns.Length == 0) return null;

        Transform best = spawns[0];
        float bestDist = Vector3.Distance(pos, best.position);

        foreach (var s in spawns)
        {
            float d = Vector3.Distance(pos, s.position);
            if (d < bestDist)
            {
                best = s;
                bestDist = d;
            }
        }

        return best;
    }

    private void RepositionEnemiesAwayFromPlayer()
    {
        if (enemySpawnPoints == null || enemySpawnPoints.Length == 0)
        {
            Debug.Log("[RespawnManager] No hay enemySpawnPoints; no se reubicar�n enemigos.");
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null || enemies.Length == 0)
        {
            Debug.Log("[RespawnManager] No se encontraron enemigos activos en la escena.");
            return;
        }

        Debug.Log("[RespawnManager] Reubicando " + enemies.Length + " enemigos lejos del jugador.");

        List<Transform> available = new List<Transform>(enemySpawnPoints);

        foreach (var e in enemies)
        {
            Transform chosen = null;

            // Buscar un spawn disponible que est� suficientemente lejos
            for (int i = 0; i < available.Count; i++)
            {
                if (Vector3.Distance(available[i].position, player.transform.position) >= minEnemyDistanceFromPlayer)
                {
                    chosen = available[i];
                    available.RemoveAt(i);
                    break;
                }
            }

            // Si no hay ninguno suficientemente lejos, elegir el spawn con mayor distancia
            if (chosen == null)
            {
                float bestDist = -1f;
                foreach (var s in enemySpawnPoints)
                {
                    float d = Vector3.Distance(s.position, player.transform.position);
                    if (d > bestDist)
                    {
                        bestDist = d;
                        chosen = s;
                    }
                }
            }

            if (chosen != null)
            {
                e.transform.position = chosen.position;
                var rbE = e.GetComponent<Rigidbody2D>();
                if (rbE != null) rbE.linearVelocity = Vector2.zero;
            }
        }
    }
}