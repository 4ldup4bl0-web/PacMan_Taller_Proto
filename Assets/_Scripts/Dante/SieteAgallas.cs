using UnityEngine;

public class SieteAgallas : MonoBehaviour
{
    // VARIABLES DE PERSISTENCIA
    public float extraChaseTime = 5f;

    // REFERENCIAS DE SCRIPTS
    private RandomMovement randomMovementScript; // Movimiento aleatorio y detección de pared
    private EnemyFoundYou chaseScript;              // Detección de jugador y persecución
    private float originalDetectionTime;            // Tiempo de olvido base

    private void Awake()
    {
        randomMovementScript = GetComponent<RandomMovement>();
        chaseScript = GetComponent<EnemyFoundYou>();

        if (chaseScript == null || randomMovementScript == null)
        {
            enabled = false;
            Debug.LogError("SieteAgallas requiere los scripts 'EnemigoAleatorio' y 'EnemyFoundYou'.");
            return;
        }

        // Guardar el tiempo de olvido original de EnemyFoundYou
        originalDetectionTime = chaseScript.forgetTime;

        // Aplicar la persistencia y configurar el estado inicial
        ApplyPersistence();

        // El enemigo comienza moviéndose aleatoriamente
        SetMovementState(true);
    }

    private void ApplyPersistence()
    {
        // Aumentar el tiempo de olvido en el script de persecución
        chaseScript.forgetTime = originalDetectionTime + extraChaseTime;
    }

    // Método para cambiar entre el estado aleatorio y el de persecución
    public void SetMovementState(bool isRandom)
    {
        // La lógica de movimiento aleatorio (EnemigoAleatorio) se activa/desactiva
        randomMovementScript.enabled = isRandom;

        // El script de persecución (EnemyFoundYou) debe permanecer ACTIVO para poder detectar al jugador
        // incluso mientras patrulla.

        if (isRandom)
        {
            // Volver a movimiento aleatorio: forzamos inmediatamente una dirección libre
            randomMovementScript.ForceNewRandomDirection();
        }
    }
}