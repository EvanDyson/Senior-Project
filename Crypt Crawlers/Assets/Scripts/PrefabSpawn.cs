using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public float minXOffset = -5f;
    public float maxXOffset = 5f;

    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    private float nextSpawnTime;
    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Check if it's time to spawn
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefabWithRandomOffset();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnPrefabWithRandomOffset()
    {
        // Calculate random offset
        float randomXOffset = Random.Range(minXOffset, maxXOffset);

        // Spawn prefab with random offset
        Vector3 spawnPosition = transform.position + new Vector3(randomXOffset, 0f, 0f);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}

