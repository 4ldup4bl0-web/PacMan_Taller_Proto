using UnityEngine;

public class ObjectLimiter : MonoBehaviour
{
    [Header("Objeto que quieres limitar")]
    public GameObject prefab;

    [Header("Límite máximo permitido")]
    public int maxCount = 1;

    [Header("Punto de spawn (opcional)")]
    public Transform spawnPoint;

    private int currentCount = 0;

    private void Start()
    {
        // Opcional: Spawn inicial
        TrySpawn();
    }

    public bool TrySpawn()
    {
        if (currentCount >= maxCount)
        {
            Debug.Log("No se puede spawn: Límite alcanzado.");
            return false; // No se puede crear
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        // Cuando se destruye, el contador baja
        ObjectLimiterTracker tracker = obj.AddComponent<ObjectLimiterTracker>();
        tracker.limiter = this;

        currentCount++;

        return true;
    }

    // Llamado automáticamente cuando uno de los objetos se destruye
    public void OnObjectDestroyed()
    {
        currentCount--;
        if (currentCount < 0) currentCount = 0;
    }
}
