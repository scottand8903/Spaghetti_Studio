using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Assign this in the Unity Inspector
    public bool limitEnabled = false;
    public int maxItems = 1000;
    private int currentItemCount = 0;

    public void SpawnItems(int count)
    {
        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned in the ItemSpawner script!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (limitEnabled && currentItemCount >= maxItems)
            {
                Debug.LogWarning("Item spawn limit reached.");
                break;
            }

            Vector3 randomPosition = GetRandomPosition();
            GameObject newItem = Instantiate(itemPrefab, randomPosition, Quaternion.identity, transform);
            currentItemCount++;
            Debug.Log($"Spawned {newItem.name} at position {randomPosition}");
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Define your spawning area here. For example:
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        return new Vector3(x, y, 0f);
    }
}
