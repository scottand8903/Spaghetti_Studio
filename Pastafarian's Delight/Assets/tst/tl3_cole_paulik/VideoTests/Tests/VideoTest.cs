using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ItemStressTest
{
    private int itemCount = 10000; // Number of items to spawn

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator StressTest_ItemSpawning()
    {
        yield return new WaitForSeconds(1); // Wait for scene to load

        // Count existing items before spawning new ones
        int initialItemCount = Object.FindObjectsOfType<ItemUse>().Length;

        // Create a parent container to hold spawned items
        GameObject itemContainer = new GameObject("ItemContainer");

        // Spawn new items
        for (int i = 0; i < itemCount; i++)
        {
            GameObject itemObj = new GameObject("Item_" + i);
            itemObj.AddComponent<ItemUse>(); // Attach ItemUse script
            itemObj.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            itemObj.transform.parent = itemContainer.transform;
        }

        yield return new WaitForSeconds(1); // Allow items to initialize

        // Count total items after spawning
        int finalItemCount = Object.FindObjectsOfType<ItemUse>().Length;
        int spawnedItemCount = finalItemCount - initialItemCount;

        // Verify only the expected number of items were added
        Assert.AreEqual(itemCount, spawnedItemCount, $"Expected {itemCount} items, but found {spawnedItemCount}.");

        Debug.Log($"Stress test completed: {spawnedItemCount} items successfully processed.");
    }
}
