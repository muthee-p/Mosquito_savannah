using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array of prefabs to spawn
    public int[] minCounts; // Array of minimum counts for each prefab
    public int[] maxCounts; // Array of maximum counts for each prefab

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            int count = Random.Range(minCounts[i], maxCounts[i] + 1); // Random count within the specified range

            for (int j = 0; j < count; j++)
            {
                Vector3 randomPosition = GetRandomPositionOnTerrain();
                GameObject prefabInstance = Instantiate(prefabs[i], randomPosition, Quaternion.identity);
                spawnedPrefabs.Add(prefabInstance);
            }
        }
    }

    Vector3 GetRandomPositionOnTerrain()
    {
        // Replace this with your actual method of getting a random position on your terrain
        // For example, you can use Random.Range to get random x and z coordinates, and then use Terrain.SampleHeight to get the y coordinate
        float randomX = Random.Range(95f, 312f);
        float randomZ = Random.Range(53f, 288f);
        float y = Random.Range(18f, 18.3f);
        return new Vector3(randomX, y, randomZ);
    }

    // Call this method whenever a prefab is destroyed and you want to spawn another one
    void RespawnPrefab(GameObject prefab)
    {
        spawnedPrefabs.Remove(prefab);

        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i] == prefab)
            {
                Vector3 randomPosition = GetRandomPositionOnTerrain();
                GameObject prefabInstance = Instantiate(prefab, randomPosition, Quaternion.identity);
                spawnedPrefabs.Add(prefabInstance);
                break;
            }
        }
    }
}
