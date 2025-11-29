using UnityEngine;

public class EnemyFoundYou : MonoBehaviour
{
    public Transform playerLocation;
    public float baseMovementSpeed = 5f;
    public float chaseSpeedMultiplier = 1.5f;
    public float detectionRange = 10f;

    // Variable que SieteAgallas modifica
    public float forgetTime = 1f;

    private float currentMovementSpeed;
    private Vector2 lastKnownPlayerPosition;
    private float chaseTimer;

    private SieteAgallas controller;

    private void Awake()
    {
        currentMovementSpeed = baseMovementSpeed;
        controller = GetComponent<SieteAgallas>();

        if (playerLocation == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerLocation = player.transform;
        }
    }

    public void Update()
    {
        if (playerLocation == null) return;

        float playerDistance = Vector2.Distance(transform.position, playerLocation.position);
        bool playerDetected = playerDistance <= detectionRange;

        if (playerDetected)
        {
            chaseTimer = forgetTime;
            currentMovementSpeed = baseMovementSpeed * chaseSpeedMultiplier;
            lastKnownPlayerPosition = playerLocation.position;
            controller.SetMovementState(false); // Desactiva movimiento aleatorio
        }

        if (chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = lastKnownPlayerPosition;
            Vector2 lookDirection = targetPosition - currentPosition;

            Vector2 movement = Vector2.zero;
            float distanceX = Mathf.Abs(lookDirection.x);
            float distanceY = Mathf.Abs(lookDirection.y);

            if (distanceX >= distanceY)
            {
                float directionX = Mathf.Sign(lookDirection.x);
                movement = new Vector2(directionX, 0);
            }
            else
            {
                float directionY = Mathf.Sign(lookDirection.y);
                movement = new Vector2(0, directionY);
            }

            transform.position += (Vector3)(movement * currentMovementSpeed * Time.deltaTime);

            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            if (chaseTimer <= 0)
            {
                currentMovementSpeed = baseMovementSpeed;

                lastKnownPlayerPosition = Vector2.zero;

                controller.SetMovementState(true); // Vuelve a movimiento Aleatorio y fuerza el cambio de dirección
            }
        }
    }
}