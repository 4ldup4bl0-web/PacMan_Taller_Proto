using UnityEngine;

public class Tigre : MonoBehaviour
{
    public Transform playerLocation;
    public float movementSpeed = 4f;

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
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = playerLocation.position;

            Vector2 lookDirection = targetPosition - currentPosition;
            float distanceX = Mathf.Abs(lookDirection.x);
            float distanceY = Mathf.Abs(lookDirection.y);

            Vector2 movement = Vector2.zero;

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

            transform.position += (Vector3)(movement * movementSpeed * Time.deltaTime);
            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
}