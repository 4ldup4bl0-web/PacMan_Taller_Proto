using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    // Variables para el Power-Up
    public bool hasPowerUp = false;
    public float powerUpDuration = 10f;

    private float powerUpTimer = 0f;

    // Asumiendo que hay una colisión/trigger que activa este power-up (ej. comer una bolita de poder)
    public void ActivatePowerUp()
    {
        hasPowerUp = true;
        powerUpTimer = powerUpDuration;
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
        Debug.Log("Power-Up ha terminado.");
    }
}