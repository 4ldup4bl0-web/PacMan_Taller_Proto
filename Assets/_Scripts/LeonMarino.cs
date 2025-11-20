using UnityEngine;

public class LeonMarino : MonoBehaviour
{
    public float restDuration = 3f;// Tiempo que duerme
    public float chanceInterval = 5f;// Cada cuanto tiempo verá si duerme o no
    private bool isIdle = false;
    private float idleTimer = 0f;
    private float chanceTimer = 0f;

    // Referencia al componente de movimiento del enemigo
    private Tigre enemyMovement;// Cambiar al script de movimiento del enemigo
    private float originalMovementSpeed;

    private void Awake()
    {
        enemyMovement = GetComponent<Tigre>();// Cambiar al script de movimiento tambien

        if (enemyMovement != null)
        {
            // La velocidad original para restaurarla después de la pausa
            originalMovementSpeed = enemyMovement.movementSpeed;
        }
        else
        {
            enabled = false; // Desactiva este script si no encuentra el de movimiento
        }
    }

    public void Update()
    {
        if (isIdle)
        {
            // El enemigo está inactivo, incrementa el temporizador
            idleTimer += Time.deltaTime;

            if (idleTimer >= restDuration)
            {
                ExitIdleState();
            }
        }
        else
        {
            // === CHEQUEO DE PROBABILIDAD ===
            chanceTimer += Time.deltaTime;

            if (chanceTimer >= chanceInterval)
            {
                // Chequeo de la probabilidad (1/2, 50%)
                if (Random.Range(0, 2) == 1)
                {
                    EnterIdleState();
                }

                // Reinicia el temporizador de chequeo, independientemente del resultado
                chanceTimer = 0f;
            }
        }
    }

    // Método para iniciar el estado de dormir
    private void EnterIdleState()
    {
        isIdle = true;
        // DETENEMOS EL MOVIMIENTO: Establecemos la velocidad del script de movimiento a cero
        enemyMovement.movementSpeed = 0f;
        Debug.Log("Leon Marino entra en estado de pausa.");
    }

    // Método para salir del estado de pausa
    private void ExitIdleState()
    {
        isIdle = false;
        idleTimer = 0f;
        // RESTAURAMOS EL MOVIMIENTO: Devolvemos la velocidad a su valor original
        enemyMovement.movementSpeed = originalMovementSpeed;
        Debug.Log("Leon Marino vuelve a la persecución.");
    }
}
