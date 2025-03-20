using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Assign this in Unity
    private List<GameObject> spawnedItems = new List<GameObject>();

    public bool limitEnabled = true; // Default: limit enforced
    private const int MAX_ITEMS = 1000;

    public void SpawnItems(int count)
    {
        int spawnCount = count;

        // Apply the item limit only if enabled
        if (limitEnabled)
        {
            int availableSpots = MAX_ITEMS - spawnedItems.Count;
            spawnCount = Mathf.Min(count, availableSpots); // Prevent exceeding MAX_ITEMS
        }

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, 
                new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), 
                Quaternion.identity);

            newItem.name = "Item_" + (spawnedItems.Count + 1);
            newItem.transform.parent = transform;

            spawnedItems.Add(newItem);
        }
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
