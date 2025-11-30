using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    // Variables para el Power-Up
    public bool hasPowerUp = false;
    public float powerUpDuration = 10f;

    private float powerUpTimer = 0f;
    private Pacman pacmanScript; // Referencia al script Pacman para control visual

    private void Awake()
    {
        // Buscar el script Pacman en el mismo GameObject
        pacmanScript = GetComponent<Pacman>();
    }
    
    // Método PÚBLICO para ser llamado por el Power-Up coleccionable
    public void ActivatePowerUp()
    {
        hasPowerUp = true;
        powerUpTimer = powerUpDuration;
        
        // Llamar al método del script Pacman para cambiar el sprite
        if (pacmanScript != null)
        {
            pacmanScript.SetPowerUpVisuals(true);
        }
        
        Debug.Log("¡Power-Up activado! Tiempo restante: " + powerUpDuration);
    }

    private void Update()
    {
        if (hasPowerUp)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                DeactivatePowerUp();
            }
        }
    }

    private void DeactivatePowerUp()
    {
        hasPowerUp = false;
        
        // Llamar al método del script Pacman para restaurar el sprite
        if (pacmanScript != null)
        {
            pacmanScript.SetPowerUpVisuals(false);
        }
        
        Debug.Log("Power-Up ha terminado.");
    }
}