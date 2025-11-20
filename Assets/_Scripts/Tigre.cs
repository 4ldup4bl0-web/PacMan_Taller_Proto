using UnityEngine;

public class Tigre : MonoBehaviour
{
    public Transform playerLocation;
    public float movementSpeed = 9f;// public o private con la velocidad base del enemigo

    private void Awake()
    {
        if (playerLocation == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerLocation = player.transform;
            }
        }
    }

    public void Update()
    {
        if (playerLocation != null)
        {
            // Posición del jugador
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = playerLocation.position;

            // El enemigo va hacia la posición actual del jugador
            transform.position = Vector2.MoveTowards(
                currentPosition,
                targetPosition,
                movementSpeed * Time.deltaTime
            );

            // Rotar el sprite para que mire en la dirección del movimiento
            Vector2 lookDirection = targetPosition - currentPosition;
            if (lookDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

        }
    }
}
