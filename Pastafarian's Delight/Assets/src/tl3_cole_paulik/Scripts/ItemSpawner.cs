using UnityEngine;
using System.Collections.Generic;


public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public bool limitEnabled = false;
    [SerializeField] public int maxItems = 100;
    [SerializeField] protected float spawnInterval = 2f;
    [SerializeField] private Dictionary<int, GameObject> spawnedItems = new Dictionary<int, GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnItems), spawnInterval, spawnInterval);
    }

    public void SpawnItems(int count)
    {
        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned!");
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

            GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            newItem.name = $"{itemPrefab.name}_{spawned + 1}";
            
            spawnedItems.Add(spawned, newItem);
            spawned++;

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
