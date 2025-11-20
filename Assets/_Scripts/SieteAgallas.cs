using UnityEngine;

public class SieteAgallas : MonoBehaviour
{
    public float extraChaseTime = 5f; // Puedes ajustar este valor en el Inspector

    // Cambiar al nombre de movimiento de los enemigos
    private Enemy chaseScript;

    private void Awake()
    {
        // 1. Obtener la referencia del script de persecución
        chaseScript = GetComponent<Enemy>();

        if (chaseScript == null)
        {
            enabled = false; // Desactiva este script si falta el componente principal
            return;
        }

        // 2. Aumentar el tiempo de reinicio de la persecución
        // La variable de movimiento de los enemigos debe ser pública para ser modificada por este script

        // Si tu script tiene una función para establecer este tiempo, úsala:
        // chaseScript.SetResetTime(chaseScript.GetResetTime() + extraChaseTime);
        ApplyPersistence();
    }

    // Si el script común de cada enemigo usa un método para reiniciar el temporizador
    // cuando el jugador sale del rango, asegúrate de que use este nuevo valor.

    private void ApplyPersistence()
    {
        // **ESTO ES UN EJEMPLO. DEBES AJUSTARLO AL NOMBRE DE TU PROPIA VARIABLE**

        // Simulación de la modificación de la variable:
        // float originalTime = chaseScript.chaseResetTime; 
        // chaseScript.chaseResetTime = originalTime + extraChaseTime;
        // la mejor práctica es usar un método público en el script base.

    }

    // Opcional: Si el script de persecución tiene un evento o método para
    // cuando entra en el estado de "Visto", podrías modificar el tiempo
    // *justo en ese momento* y luego restaurarlo al salir.
}
