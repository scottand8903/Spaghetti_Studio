using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class StressTest
{
    private GameObject spawnerObject;
    private EnemySpawner spawner;
    private int spawnedEnemies = 0;
    private int maxEnemiesBeforeLag = 0;
    private float fpsThreshold = 30f; // Define significant performance dip (e.g., below 30 FPS)
    private float stressSpawnRate = 0.02f; // How quickly enemies spawn

    [OneTimeSetUp]
    
    public void LoadScene()
    {
        SceneManager.LoadScene("EnemyTests");
    }


    [UnityTest]
    public IEnumerator NumberofEnemiesStressTest()
    {
        spawner = GameObject.FindObjectOfType<EnemySpawner>();
        Debug.Log("Starting Enemy Spawn Stress Test...");

        while (true)
        {
            Debug.Log($"Spawning Enemy: {spawnedEnemies + 1}");
            // Spawn an enemy
            spawner.SpawnEnemy();
            spawnedEnemies++;

            // Wait before measuring FPS
            yield return new WaitForEndOfFrame(); // Ensures Unity updates frame data before measuring FPS

            // Measure FPS
            float currentFPS = 1f / Time.deltaTime;
            Debug.Log($"Enemies: {spawnedEnemies}, FPS: {currentFPS}");

            // Check if FPS drops below threshold
            if (currentFPS < fpsThreshold)
            {
                maxEnemiesBeforeLag = spawnedEnemies;
                Debug.Log($"Performance Drop Detected: {maxEnemiesBeforeLag} Enemies at {currentFPS} FPS");
                break;
            }

            yield return new WaitForSeconds(stressSpawnRate);
        }

        // Log the result
        Debug.Log($"Max Enemies Before Performance Dip: {maxEnemiesBeforeLag}");
        Assert.Greater(maxEnemiesBeforeLag, 0, "Enemies caused performance drop too quickly!");

        // Cleanup
        Object.Destroy(spawnerObject);
    }
}
