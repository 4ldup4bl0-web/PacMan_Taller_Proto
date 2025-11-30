using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PacmanUpgrade pacmanUpgrade;
    private Movement movement;

    private bool powerUpActive = false;
    private float powerUpTimer = 0f;

    // CONFIGURABLES
    public float powerUpDuration = 8f;
    public float speedBoostMultiplier = 1.8f;

    private float normalSpeed;

    private void Awake()
    {
        pacmanUpgrade = GetComponent<PacmanUpgrade>();
        movement = GetComponent<Movement>();

        normalSpeed = movement.speed;
    }

    private void Update()
    {
        if (powerUpActive)
        {
            powerUpTimer -= Time.deltaTime;

            if (powerUpTimer <= 0f)
            {
                DeactivatePowerUp();
            }
        }
    }

    // ✅ LLAMADO DESDE PowerUp.cs
    public void ActivatePowerUp()
    {
        if (powerUpActive)
        {
            powerUpTimer = powerUpDuration;
            return;
        }

        powerUpActive = true;
        powerUpTimer = powerUpDuration;

        // Cambiar sprite
        pacmanUpgrade.ActivatePowerUp();

        // Aumentar velocidad
        movement.speed = normalSpeed * speedBoostMultiplier;

        // Aquí activas que Pacman coma enemigos
        // (Solo si tienes un sistema de ghosts)
        Debug.Log("POWER-UP ACTIVADO: Pacman puede comer enemigos");
    }

    public void DeactivatePowerUp()
    {
        powerUpActive = false;

        // Restaurar sprite
        pacmanUpgrade.DeactivatePowerUp();

        // Restaurar velocidad
        movement.speed = normalSpeed;

        Debug.Log("POWER-UP FINALIZADO");
    }

    public bool IsPowered()
    {
        return powerUpActive;
    }
}
