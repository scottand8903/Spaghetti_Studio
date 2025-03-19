using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Drag your ItemPrefab here in the Unity Editor
    private List<GameObject> spawnedItems = new List<GameObject>();

    // Removed maxItems limit for stress testing
    public void SpawnItems(int count)
    {
        for (int i = 0; i < count; i++)
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

