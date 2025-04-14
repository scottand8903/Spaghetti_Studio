using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints; // Places where items can appear
    [SerializeField] public GameObject itemPrefab; // Default item
    [SerializeField] public GameObject speedItemPrefab;
    [SerializeField] public GameObject regenerationPowerUpPrefab;

    [SerializeField] public bool limitEnabled = false; // Enable/disable item limit
    [SerializeField] public int maxItems = 100; // Maximum number of items if limitEnabled
    [SerializeField] protected float spawnInterval = 2f; // Time between spawns

    private Dictionary<int, GameObject> spawnedItems = new Dictionary<int, GameObject>();

    private void Start()
    {
        // Repeatedly call SpawnItems at a set interval
        InvokeRepeating(nameof(SpawnItems), spawnInterval, spawnInterval);
    }

    public void SpawnItems(int count)
    {
        // Check for missing references
        if (itemPrefab == null || speedItemPrefab == null || regenerationPowerUpPrefab == null)
        {
            Debug.LogError("One or more item prefabs are not assigned!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        int spawned = 0;

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawned >= count)
                break;

            // Enforce item cap if enabled
            if (limitEnabled && spawnedItems.Count >= maxItems)
            {
                Debug.LogWarning("Item spawn limit reached.");
                return;
            }

            // Skip if this index already has an item
            if (spawnedItems.ContainsKey(spawned))
                continue;

            GameObject itemToSpawn = GetRandomItem();

            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            newItem.name = $"{itemToSpawn.name}_{spawned + 1}";

            spawnedItems.Add(spawned, newItem);
            spawned++;

            Debug.Log($"Spawned {newItem.name} at {spawnPoint.position}");
        }
    }

    private GameObject GetRandomItem()
    {
        // Randomly pick one of the three item types
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return itemPrefab;
            case 1:
                return speedItemPrefab;
            case 2:
                return regenerationPowerUpPrefab;
            default:
                return itemPrefab;
        }
    }

    public void ClearSpawnedItems()
    {
        // Delete all items and clear the dictionary
        foreach (var item in spawnedItems.Values)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
        Debug.Log("All spawned items have been cleared.");
    }
}
