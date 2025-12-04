using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public List<Image> lives;
    private int currentLives;

    void Start()
    {
        currentLives = lives != null ? lives.Count : 0;
        refreshUI();
    }

    public void refreshUI()
    {
        if (lives == null) return;
        for (int i = 0; i < lives.Count; i++)
            lives[i].enabled = (i < currentLives);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Solo nos interesan colisiones con enemigos
        if (!other.CompareTag("Enemy")) return;

        Debug.Log("[Lives] Colisión con Enemy detectada: " + other.name);

        // 1) Si el jugador está en invencibilidad temporal (iframes), ignorar el golpe
        PlayerInvincibility inv = GetComponent<PlayerInvincibility>();
        if (inv != null && inv.IsInvincible())
        {
            Debug.Log("[Lives] Ignorado: jugador está invencible por temporizador (iframes).");
            return;
        }

        // 2) Si el jugador tiene un power-up activo (chequeamos tus posibles scripts: Chainsaw y PlayerState)
        // -> no recibe daño (comportamiento solicitado)
        Chainsaw chainsaw = GetComponent<Chainsaw>();
        if (chainsaw != null && chainsaw.hasPowerUp)
        {
            Debug.Log("[Lives] Ignorado: Chainsaw power-up activo.");
            return;
        }

        PlayerState playerState = GetComponent<PlayerState>();
        if (playerState != null && playerState.IsPowered())
        {
            Debug.Log("[Lives] Ignorado: PlayerState power-up activo.");
            return;
        }

        // Si llegamos aquí, el jugador sí recibe daño: restar vida y reaccionar
        if (currentLives > 0)
        {
            currentLives--;
            refreshUI();
            Debug.Log("[Lives] Vida perdida. Vidas restantes: " + currentLives);
            if (SFXManager.Instance != null)
                SFXManager.Instance.PlayPlayerHurt();

            if (currentLives > 0)
            {
                // Activar invencibilidad breve al recibir daño para evitar multi-hit
                if (inv != null)
                {
                    inv.TriggerInvincibility();
                    Debug.Log("[Lives] Invencibilidad por i-frames activada tras recibir daño.");
                }

                if (RespawnManager.Instance != null)
                {
                    RespawnManager.Instance.HandlePlayerHit();
                }
                else
                {
                    Debug.LogWarning("[Lives] RespawnManager.Instance es null.");
                }
            }
            else
            {
                Debug.Log("[Lives] No quedan vidas -> Game Over.");
                if (GameOverManager.Instance != null)
                    GameOverManager.Instance.ShowGameOver();
            }
        }
    }
}