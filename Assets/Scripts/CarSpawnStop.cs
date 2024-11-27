using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnStop : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnTransform; // The point where cars spawn
        public Vector3 direction; // The direction the car should drive
    }

    public SpawnPoint[] spawnPoints; // Array of spawn points
    public GameObject carPrefab; // The car prefab to spawn
    public float moveSpeed = 5f; // Speed of the car
    public float minSpawnInterval = 2f; // Minimum interval between spawns
    public float maxSpawnInterval = 5f; // Maximum interval between spawns

    void Start()
    {
        // Start the car spawning coroutine
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            if (carPrefab == null)
            {
                Debug.LogError("carPrefab is not assigned!");
                yield break;
            }

            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogError("spawnPoints are not assigned or empty!");
                yield break;
            }

            // Choose a random spawn point
            SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn a car at the spawn point
            GameObject car = Instantiate(carPrefab, spawnPoint.spawnTransform.position, spawnPoint.spawnTransform.rotation);
            Debug.Log("Car spawned: " + car.name);
            CarMovement carMovement = car.GetComponent<CarMovement>();

            if (carMovement == null)
            {
                Debug.LogError("CarMovement component is missing on the car prefab!");
                yield break;
            }

            carMovement.moveSpeed = moveSpeed;
            carMovement.direction = spawnPoint.direction;

            // Wait for a random interval before spawning the next car
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}