using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject healthItemPrefab; // Assign in Unity
    public float spawnInterval = 10f; // Time between health item spawns

    private List<GameObject> spawnedItems = new List<GameObject>();

    private void Start()
    {
        // Start periodic spawning of health items
        InvokeRepeating(nameof(SpawnHealthItem), spawnInterval, spawnInterval);
    }

    private void SpawnHealthItem()
    {
        if (healthItemPrefab == null)
        {
            Debug.LogWarning("Health item prefab not assigned!");
            return;
        }

        // Random spawn position within a defined range
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        GameObject newItem = Instantiate(healthItemPrefab, spawnPosition, Quaternion.identity);

        spawnedItems.Add(newItem);
    }

    public void ClearItems()
    {
        foreach (var item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }
}
