using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiPointSpawner : MonoBehaviour
{
    public GameObject prefab;               // Objeto a instanciar
    public Transform[] spawnPoints;         // Puntos de spawn
    public float spawnInterval = 5f;        // Tiempo entre spawns
    public int maxObjects = 10;             // Límite máximo de objetos

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnInAllPoints();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnInAllPoints()
    {

        if (spawnedObjects.Count >= maxObjects)
        {
            Debug.Log("Se alcanzó el límite máximo de objetos.");
            return;
        }

        foreach (Transform point in spawnPoints)
        {

            if (spawnedObjects.Count >= maxObjects)
                break;

            GameObject obj = Instantiate(prefab, point.position, point.rotation);
            spawnedObjects.Add(obj);


        }

        Debug.Log("Objetos creados. Total actual: " + spawnedObjects.Count);
    }
}

