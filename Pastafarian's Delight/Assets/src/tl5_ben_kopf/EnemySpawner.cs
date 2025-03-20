using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab; // Assign the enemy prefab in the Inspector
    [SerializeField] public Transform[] spawnPoints; // Assign multiple spawn points
    [SerializeField] protected float spawnInterval = 2f; // Time between spawns
    [SerializeField] private int maxEnemies = 10; // Limit the number of enemies

    [SerializeField] private int currentEnemyCount = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    public void SpawnEnemy()
    {

        if (currentEnemyCount >= maxEnemies)
            return;

        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogError("No spawn points or enemy prefab assigned!");
            return;
        }

        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f); // Set Z to 0
        // Spawn the enemy and increase count
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        Debug.Log("Spawned " + newEnemy.name + " at " + spawnPosition);
    }
}

