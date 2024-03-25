using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnArea1 : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public BoxCollider spawnArea;
    public float spawnInterval = 3f;
    public int maxSpawnCount = 10;

    public float fixedYPosition = 1f; // Fixed Y position for spawning


    private float timer = 0f;
    private List<Transform> spawnedInstances = new List<Transform>();
    public bool spawnerActive;

    void Start()
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            SpawnPrefabRandomly();
            CheckRespawnInstances();
        }

        spawnerActive = true;
    }

    void Update()
    {
        if (spawnerActive)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval && spawnedInstances.Count < maxSpawnCount)
            {
                SpawnPrefabRandomly();
                timer = 0f;
            }

            CheckRespawnInstances();
        }

        else
        {
            timer = 0f;
        }
    }

    void SpawnPrefabRandomly()
    {
        Vector3 randomSpawnPoint = GetRandomPointInCollider();

        // Set Y position to the fixed value
        randomSpawnPoint.y = fixedYPosition;


        GameObject randomPrefab = prefabToSpawn[Random.Range(0, prefabToSpawn.Length)];
        GameObject newPrefabInstance = Instantiate(randomPrefab, randomSpawnPoint, Quaternion.identity);
        spawnedInstances.Add(newPrefabInstance.transform);
    }

    void CheckRespawnInstances()
    {
        for (int i = 0; i < spawnedInstances.Count; i++)
        {
            if (spawnedInstances[i] == null)
            {
                spawnedInstances.RemoveAt(i);
                SpawnPrefabRandomly();
            }
        }
    }

    Vector3 GetRandomPointInCollider()
    {
        float randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float randomZ = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(randomX, 0f, randomZ);
    }
}

