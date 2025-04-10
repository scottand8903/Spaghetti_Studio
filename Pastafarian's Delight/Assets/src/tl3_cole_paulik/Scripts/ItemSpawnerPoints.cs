using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemSpawnerPoints : MonoBehaviour
{
    // Array of spawn points where items can appear
    public Transform[] spawnPoints;

    // Prefabs for the item GameObjects
    public GameObject HealingItemPrefab;
    public GameObject speedItemPrefab; // Added Speed Item Prefab
    public GameObject regenerationPowerUpPrefab;

    // Dictionary to track spawned items and their positions
    private Dictionary<int, GameObject> spawnedItems = new Dictionary<int, GameObject>();

    void Start()
    {
        LoadOrSpawnItems();
    }

    void LoadOrSpawnItems()
    {
        spawnedItems.Clear();
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            // Randomly choose between three item types
            GameObject itemToSpawn = GetRandomItem();

            if (itemToSpawn == null)
            {
                Debug.LogError("One or more item prefabs are not assigned!");
                continue;
            }

            // Instantiate the chosen item at the spawn point
            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            newItem.name = $"{itemToSpawn.name}_{i + 1}";

            // Store reference to spawned item
            spawnedItems[i] = newItem;

            Debug.Log($"Spawned {newItem.name} at {spawnPoint.position}");
        }
    }

    // Method to randomly choose between three items
    private GameObject GetRandomItem()
    {
        // Randomly choose between three items
        int randomIndex = Random.Range(0, 3); // 0, 1, or 2
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
        foreach (var item in spawnedItems.Values)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
        Debug.Log("All spawned items have been cleared.");
    }
}
