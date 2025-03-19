using UnityEngine;

public class EnemySpawnerMelee : MonoBehaviour
{
    [SerializeField] private GameObject meleeEnemyPrefab; // Assign the melee enemy prefab in the Inspector
    [SerializeField] private GameObject tankMeleeEnemyPrefab; // Assign the tank enemy prefab in the Inspector
    [SerializeField] private GameObject speedMeleeEnemyPrefab; // Assign the speed enemy prefab in the Inspector
    [SerializeField] private Transform[] spawnPoints; // Assign multiple spawn points
    [SerializeField] private float spawnInterval = 2f; // Time between spawns
    [SerializeField] private int maxEnemies = 10; // Limit the number of enemies

    [SerializeField] private int currentEnemyCount = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points or enemy prefab assigned!");
            return;
        }

        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f); // Set Z to 0
        // Spawn the enemy and increase count
        GameObject enemyPrefab = GetRandomEnemyPrefab();

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        Debug.Log("Spawned " + newEnemy.name + " at " + spawnPosition);
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomChoice = Random.Range(0, 3); // 0-3 for 4 enemy types

        if (randomChoice == 0) return meleeEnemyPrefab;
        if (randomChoice == 1) return tankMeleeEnemyPrefab;
        return speedMeleeEnemyPrefab;
    }
}

