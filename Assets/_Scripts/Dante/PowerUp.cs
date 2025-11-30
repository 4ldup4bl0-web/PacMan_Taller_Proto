using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el script PlayerState del jugador
            PlayerState playerState = other.GetComponent<PlayerState>();

            if (playerState != null)
            {
                // Activar el power-up en el jugador
                playerState.ActivatePowerUp();

                // Destruir el power-up de la escena
                Destroy(gameObject);
            }
        }
    }
}