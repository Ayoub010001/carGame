
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadSegmentPrefab;
    public GameObject obstaclePrefab; // Prefab for obstacle
    public GameObject coinPrefab; // Prefab for coin
    public Transform car;
    public float segmentLength = 10f;
    public int segmentsToSpawn = 5;
    public float despawnOffset = 5f; // Offset to determine when to despawn road segments
    public float roadWidth = 4f; // Width of the road

    private float spawnZ = 0f;
    private List<GameObject> spawnedSegments = new List<GameObject>();
    private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to store spawned obstacles
    private List<GameObject> spawnedCoins = new List<GameObject>(); // List to store spawned coins
    private int collectedCoins = 0;

    private void Start()
    {
        SpawnRoad();
    }

    private void Update()
    {
        if (car.position.z > spawnZ - segmentsToSpawn * segmentLength)
        {
            SpawnRoad();
            DespawnRoad();
            SpawnObstacles(); // Spawn obstacles
            SpawnCoins(); // Spawn coins
        }
    }

    private void SpawnRoad()
    {
        for (int i = 0; i < segmentsToSpawn; i++)
        {
            GameObject newSegment = Instantiate(roadSegmentPrefab, transform.forward * spawnZ, transform.rotation);
            newSegment.SetActive(true);
            spawnedSegments.Add(newSegment);
            spawnZ += segmentLength;
        }
    }

    private void DespawnRoad()
    {
        List<GameObject> segmentsToRemove = new List<GameObject>();

        foreach (var segment in spawnedSegments)
        {
            if (car.position.z - segment.transform.position.z > despawnOffset)
            {
                segmentsToRemove.Add(segment);
                Destroy(segment);
            }
        }

        foreach (var segmentToRemove in segmentsToRemove)
        {
            spawnedSegments.Remove(segmentToRemove);
        }
    }

    private void SpawnObstacles()
    {
        // Determine the number of obstacles to spawn based on the number of road segments
        int obstaclesToSpawn = segmentsToSpawn / 2; // You can adjust this value for difficulty

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            // Randomly choose a position along the road
            float obstacleZ = Random.Range(spawnZ - segmentsToSpawn * segmentLength, spawnZ);
            float obstacleX = Random.Range(-roadWidth / 2f, roadWidth / 2f);
            Vector3 obstaclePosition = new Vector3(obstacleX, 0.1f, obstacleZ); // Adjust the Y position as needed

            // Spawn the obstacle
            GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
            spawnedObstacles.Add(obstacle); // Add obstacle to the spawnedObstacles list
        }
    }

    private void SpawnCoins()
    {
        int coinsToSpawn = segmentsToSpawn / 2;

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float coinZ = Random.Range(spawnZ - segmentsToSpawn * segmentLength, spawnZ);
            float coinX = Random.Range(-roadWidth / 2f, roadWidth / 2f);
            Vector3 coinPosition = new Vector3(coinX, 1f, coinZ);

            GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
            spawnedCoins.Add(coin); // Add coin to the spawnedCoins list
        }
    }

}



