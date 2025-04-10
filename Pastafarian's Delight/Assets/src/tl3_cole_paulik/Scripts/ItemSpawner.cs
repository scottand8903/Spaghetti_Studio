using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public GameObject speedItemPrefab; // New: Speed item prefab
    [SerializeField] public GameObject regenerationPowerUpPrefab;  // Assign in Unity Inspector

    [SerializeField] public bool limitEnabled = false;
    [SerializeField] public int maxItems = 100;
    [SerializeField] protected float spawnInterval = 2f;
    private Dictionary<int, GameObject> spawnedItems = new Dictionary<int, GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnItems), spawnInterval, spawnInterval);
    }

    public void SpawnItems(int count)
{
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

        if (limitEnabled && spawnedItems.Count >= maxItems)
        {
            Debug.LogWarning("Item spawn limit reached.");
            return;
        }

        if (spawnedItems.ContainsKey(spawned))
            continue;

        // Randomly decide which item to spawn from three options
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
    // Randomly choose between three items
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
            return itemPrefab; // Default fallback
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
