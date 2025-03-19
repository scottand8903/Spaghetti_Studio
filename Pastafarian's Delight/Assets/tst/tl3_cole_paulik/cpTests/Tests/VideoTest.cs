using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class ItemSpawnerStressTests
{
    private GameObject spawnerObject;
    private ItemSpawner itemSpawner;

    private float spawnTimeThreshold = 1f; // Threshold for performance lag (in seconds)
    private int spawnBatchSize = 10; // Start with spawning 10 items per batch
    private int totalSpawnedItems = 0; // Track total items spawned

    [SetUp]
    public void Setup()
    {
        spawnerObject = new GameObject();
        itemSpawner = spawnerObject.AddComponent<ItemSpawner>();
        itemSpawner.itemPrefab = new GameObject(); // Mock prefab (empty GameObject)
    }

    [UnityTest]
    public IEnumerator StressTest_FindBreakingPoint()
    {
        bool isBreaking = false;

        // Keep spawning items until a breaking point is reached
        while (!isBreaking)
        {
            float startTime = Time.realtimeSinceStartup;

            // Spawn a batch of items
            itemSpawner.SpawnItems(spawnBatchSize);

            // Measure how long it took to spawn the batch
            float spawnDuration = Time.realtimeSinceStartup - startTime;

            // Log the spawn attempt and duration
            Debug.Log($"Spawned {spawnBatchSize} items in {spawnDuration} seconds.");

            // Check if the spawn duration exceeded the threshold
            if (spawnDuration > spawnTimeThreshold)
            {
                isBreaking = true; // Break the loop when performance degrades
                Debug.LogError($"Breaking point reached! Took {spawnDuration} seconds to spawn {totalSpawnedItems + spawnBatchSize} items.");
            }
            else
            {
                // Increment the total items spawned and prepare for the next batch
                totalSpawnedItems += spawnBatchSize;
            }

            // Increase the spawn batch size for the next round (gradually increase stress)
            spawnBatchSize += 10;

            // Add a short delay between spawns to prevent overwhelming Unity
            yield return new WaitForSeconds(0.1f); 
        }

        // Final log of the breaking point
        Debug.Log($"Test finished. Breaking point at {totalSpawnedItems} items.");
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after the test
        Object.Destroy(spawnerObject);
    }
}
