using UnityEngine;

public class LeonMarino : MonoBehaviour
{
    public float restDuration = 3f; // Tiempo que duerme
    public float chanceInterval = 5f; // Cada cuanto tiempo verá si duerme o no

    private bool isIdle = false;
    private float idleTimer = 0f;
    private float chanceTimer = 0f;

    public RandomMovement enemyMovement;
    private float originalMovementSpeed;

    private void Awake()
    {
        enemyMovement = GetComponent<RandomMovement>();

        if (enemyMovement != null)
        {
            originalMovementSpeed = enemyMovement.movementSpeed;
        }
        else
        {
            Debug.LogError("LeonMarino requiere el script EnemigoAleatorio en el mismo GameObject.");
            enabled = false; // Desactiva este script
        }
    }

    public void Update()
    {
        if (isIdle)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= restDuration)
            {
                ExitIdleState();
            }
        }
        else
        {
            chanceTimer += Time.deltaTime;

            if (chanceTimer >= chanceInterval)
            {
                if (Random.Range(0, 2) == 1)
                {
                    EnterIdleState();
                }

                chanceTimer = 0f;
            }
        }
    }

    private void EnterIdleState()
    {
        isIdle = true;
        enemyMovement.movementSpeed = 0f;


        Debug.Log("Leon Marino entra en estado de pausa por " + restDuration + " segundos.");
    }

    private void ExitIdleState()
    {
        isIdle = false;
        idleTimer = 0f;
        enemyMovement.movementSpeed = originalMovementSpeed;


        Debug.Log("Leon Marino reanuda el movimiento aleatorio.");
    }
}