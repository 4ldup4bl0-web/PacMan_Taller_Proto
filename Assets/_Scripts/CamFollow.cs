using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Variables para seguir al jugador
    public Transform target; // El Transform (posición) del jugador
    public float smoothSpeed = 0.125f; // Controla la suavidad del movimiento de la cámara
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador (ej: (0, 0, -10))

    // Se recomienda usar LateUpdate para mover la cámara, 
    // asegurando que el jugador ya se haya movido en el Update()
    private void LateUpdate()
    {
        if (target == null)
        {
            // Opcional: Intentar encontrar al jugador si no está asignado
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                // Si aún no encuentra al jugador, simplemente salimos
                return;
            }
        }

        // Posición deseada: la posición del jugador más el desplazamiento (offset)
        Vector3 desiredPosition = target.position + offset;

        // Suavizamos el movimiento de la cámara para que no sea instantáneo
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime * 50f // Multiplicar por 50f hace que smoothSpeed sea más intuitivo
        );

        // Aplicamos la posición suavizada a la cámara
        transform.position = smoothedPosition;
    }
}