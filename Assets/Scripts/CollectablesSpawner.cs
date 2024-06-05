using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawner : MonoBehaviour
{
    public GameObject[] prefabs; 
    public int[] minCounts; 
    public int[] maxCounts; 

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
        float randomX = Random.Range(60f, 180f);
        float randomZ = Random.Range(160f, 400f);
        float y = Random.Range(18.1f, 18.4f);
        return new Vector3(randomX, y, randomZ);
    }

   public void RespawnPrefab(GameObject prefab)
    {
        Vector3 randomPosition = GetRandomPositionOnTerrain();
        GameObject prefabInstance = Instantiate(prefab, randomPosition, Quaternion.identity);
        spawnedPrefabs.Add(prefabInstance);
               
        
    }
}
