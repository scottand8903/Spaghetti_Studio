using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject healthItemPrefab; // Prefab for the health item to spawn (assigned in the Inspector)
    public float spawnInterval = 10f; // Time (in seconds) between each item spawn

    private List<GameObject> spawnedItems = new List<GameObject>(); // Keeps track of all spawned items

    private void Start()
    {
        // Calls SpawnHealthItem every 'spawnInterval' seconds, starting after 'spawnInterval' delay
        InvokeRepeating(nameof(SpawnHealthItem), spawnInterval, spawnInterval);
    }

    private void SpawnHealthItem()
    {
        // Safety check in case the prefab wasn't assigned
        if (healthItemPrefab == null)
        {
            Debug.LogWarning("Health item prefab not assigned!");
            return;
        }

        // Pick a random position within a 10x10 square area centered at (0,0)
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        // Create the item in the scene at the chosen position
        GameObject newItem = Instantiate(healthItemPrefab, spawnPosition, Quaternion.identity);

        // Track the item so it can be cleaned up later
        spawnedItems.Add(newItem);
    }

    public void ClearItems()
    {
        // Destroy all spawned items and clear the tracking list
        foreach (var item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }
}
