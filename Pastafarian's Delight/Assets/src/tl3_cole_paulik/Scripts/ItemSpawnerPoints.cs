using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemSpawnerPoints : MonoBehaviour
{
    // Array of spawn points where items can appear
    public Transform[] spawnPoints;

    // Prefab for the item GameObject
    public GameObject itemPrefab;

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

            // Instantiate the item at the spawn point
            GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            newItem.name = $"{itemPrefab.name}_{i + 1}";

            // Store reference to spawned item
            spawnedItems[i] = newItem;

            Debug.Log($"Spawned {newItem.name} at {spawnPoint.position}");
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
