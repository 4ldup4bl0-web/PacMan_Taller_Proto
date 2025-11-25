using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // VARIABLES
    public float movementSpeed = 5f;
    public float changeActionInterval = 2.0f;
    public float wallDetectionDistance = 0.6f;
    public LayerMask wallLayer;
    public int maxDirectionAttempts = 1;

    private Vector2 moveDirection = Vector2.zero;
    private float timer;
    private bool isMoving = true;

    void Start()
    {
        // Se inicializa con una dirección aleatoria que se chequeará en SieteAgallas
        ForceNewRandomDirection();
    }

    void Update()
    {
        // Lógica de temporización: solo si el script está activo (es decir, en modo aleatorio)
        timer += Time.deltaTime;
        if (timer >= changeActionInterval)
        {
            // Forzar un cambio de dirección
            ForceNewRandomDirection();
        }

        // CHEQUEO DE PARED ANTES DE MOVER
        if (moveDirection != Vector2.zero)
        {
            if (CheckForWall())
            {
                // Si detecta una pared, busca una dirección libre inmediatamente
                ForceNewRandomDirection();
                return;
            }

            // APLICAR EL MOVIMIENTO
            transform.position += (Vector3)(moveDirection * movementSpeed * Time.deltaTime);

            // ROTAR EL SPRITE
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    /// <summary>
    /// Intenta forzar una dirección ortogonal que NO esté bloqueada por una pared.
    /// Este método soluciona el problema de "cuesta moverse" al salir de la persecución.
    /// </summary>
    public void ForceNewRandomDirection()
    {
        timer = 0f;
        isMoving = true;

        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

        int attempts = 0;
        bool directionFound = false;

        while (attempts < maxDirectionAttempts && !directionFound)
        {
            int randomIndex = Random.Range(0, possibleDirections.Length);
            Vector2 chosenDirection = possibleDirections[randomIndex];

            moveDirection = chosenDirection;

            if (!CheckForWall())
            {
                directionFound = true;
            }

            attempts++;
        }

        if (!directionFound)
        {
            // Si no encuentra una salida, se detiene
            moveDirection = Vector2.zero;
        }
    }

    private bool CheckForWall()
    {
        // Lanza un rayo de detección
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            moveDirection,
            wallDetectionDistance,
            wallLayer
        );

        return hit.collider != null;
    }
}