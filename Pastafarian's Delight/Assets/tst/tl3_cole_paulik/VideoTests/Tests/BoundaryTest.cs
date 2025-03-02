using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ItemBoundaryTest
{
    private int maxItemCount = 1000;  // Upper valid limit
    private int outOfBoundsItemCount = 1001; // Out-of-bounds case

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator BoundaryTest_InBounds()
    {
        yield return new WaitForSeconds(1); // Wait for scene to load

        // Count pre-existing items
        int initialItemCount = Object.FindObjectsOfType<ItemUse>().Length;

        // Create a parent container for spawned items
        GameObject itemContainer = new GameObject("ItemContainer");

        // Spawn max allowed items
        for (int i = 0; i < maxItemCount; i++)
        {
            GameObject itemObj = new GameObject("Item_" + i);
            itemObj.AddComponent<ItemUse>();
            itemObj.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            itemObj.transform.parent = itemContainer.transform;
        }

        yield return new WaitForSeconds(1); // Allow items to initialize

        // Count total items after spawning
        int finalItemCount = Object.FindObjectsOfType<ItemUse>().Length;
        int spawnedItemCount = finalItemCount - initialItemCount;

        // Verify that exactly maxItemCount were spawned
        Assert.AreEqual(maxItemCount, spawnedItemCount, 
            $"Expected {maxItemCount} items, but found {spawnedItemCount} (Initial: {initialItemCount}, Final: {finalItemCount}).");

        Debug.Log($"In-Bounds Test Passed: {spawnedItemCount} items spawned successfully.");
    }


    [UnityTest]
    public IEnumerator BoundaryTest_OutOfBounds()
    {
        yield return new WaitForSeconds(1); // Wait for scene to load

        // Remove all existing items before starting the test
        var existingItems = Object.FindObjectsOfType<ItemUse>();
        foreach (var item in existingItems)
        {
            Object.Destroy(item.gameObject);
        }

        // Ensure that no items are left
        int initialItemCount = Object.FindObjectsOfType<ItemUse>().Length;
        Debug.Log($"Initial item count before spawning: {initialItemCount}"); // Expect 0 here

        // Create a parent container for spawned items
        GameObject itemContainer = new GameObject("ItemContainer");

        // Try spawning more than the allowed limit
        for (int i = 0; i < outOfBoundsItemCount; i++)
        {
            GameObject itemObj = new GameObject("Item_" + i);
            itemObj.AddComponent<ItemUse>();
            itemObj.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            itemObj.transform.parent = itemContainer.transform;
        }

        yield return new WaitForSeconds(1); // Allow items to initialize

        // Count total items AFTER spawning
        int finalItemCount = Object.FindObjectsOfType<ItemUse>().Length;
        int spawnedItemCount = finalItemCount - initialItemCount;

        // Verify that no more than maxItemCount were spawned
        Assert.LessOrEqual(spawnedItemCount, maxItemCount, 
            $"Expected at most {maxItemCount} items, but found {spawnedItemCount} (Initial: {initialItemCount}, Final: {finalItemCount}).");

        Debug.Log($"Out of Bounds Test Passed: Attempted {outOfBoundsItemCount} items, but game limited to {spawnedItemCount}.");
    }

}
