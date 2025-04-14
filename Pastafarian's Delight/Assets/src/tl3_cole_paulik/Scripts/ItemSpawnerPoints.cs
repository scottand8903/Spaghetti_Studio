using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemSpawnerPoints : MonoBehaviour
{
    public Transform[] spawnPoints; // Set of fixed positions where items can spawn

    // Prefabs for the different power-ups
    public GameObject HealingItemPrefab;
    public GameObject speedItemPrefab;
    public GameObject regenerationPowerUpPrefab;

    // Keeps track of which spawn point has which item
    private Dictionary<int, GameObject> spawnedItems = new Dictionary<int, GameObject>();

    void Start()
    {
        // Try to spawn one item at each spawn point
        LoadOrSpawnItems();
    }

    void LoadOrSpawnItems()
    {
        spawnedItems.Clear();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            // Randomly select a power-up prefab
            GameObject itemToSpawn = GetRandomItem();

            if (itemToSpawn == null)
            {
                Debug.LogError("One or more item prefabs are not assigned!");
                continue;
            }

            // Instantiate the item and give it a unique name
            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            newItem.name = $"{itemToSpawn.name}_{i + 1}";

            // Record which item was spawned at this spawn point
            spawnedItems[i] = newItem;

            Debug.Log($"Spawned {newItem.name} at {spawnPoint.position}");
        }
    }

    private GameObject GetRandomItem()
    {
        // Choose one of the 3 prefabs at random
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return HealingItemPrefab;
            case 1:
                return speedItemPrefab;
            case 2:
                return regenerationPowerUpPrefab;
            default:
                return HealingItemPrefab; // Fallback, should not happen
        }
    }

    public void ClearSpawnedItems()
    {
        // Destroy all current items and reset the tracking dictionary
        foreach (var item in spawnedItems.Values)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
        Debug.Log("All spawned items have been cleared.");
    }
}
