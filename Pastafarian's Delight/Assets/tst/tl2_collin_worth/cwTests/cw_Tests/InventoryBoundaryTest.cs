using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySystemTests
{


    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator AddItem_ShouldFail_WhenInventoryIsFull()
    {
        var inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        GameObject spriteObject = GameObject.Find("Ingredient"); // Find by name
        SpriteRenderer spriteRenderer = spriteObject?.GetComponent<SpriteRenderer>();
        Sprite itemSprite = spriteRenderer?.sprite;

        // Add 5 items (assuming inventory max size is 5)
        for (int i = 0; i < 5; i++)
        {
            int result = inventorySystem.addItem(i + 1, itemSprite); // Add item to inventory
            Assert.AreEqual(1, result, $"Item {i + 1} should be added successfully");
        }

        // Try to add one more item and check if the inventory is full
        int resultAfterFull = inventorySystem.addItem(6, itemSprite);
        Assert.AreEqual(0, resultAfterFull, "Item should not be added because the inventory is full");

        yield return null;
    }

    [UnityTest]
    public IEnumerator AddItem_ShouldSucceed_WhenThereIsSpace()
    {
        var inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        GameObject spriteObject = GameObject.Find("Ingredient"); // Find by name
        SpriteRenderer spriteRenderer = spriteObject?.GetComponent<SpriteRenderer>();
        Sprite itemSprite = spriteRenderer?.sprite;

        // Add 3 items (less than max capacity)
        for (int i = 0; i < 3; i++)
        {
            int result = inventorySystem.addItem(1, itemSprite);
            Assert.AreEqual(1, result, $"Item {1} should be added successfully");
        }

        // Try to add another item when there's space
        int resultAfterAdding = inventorySystem.addItem(4, itemSprite);
        Assert.AreEqual(1, resultAfterAdding, "Item should be added successfully because there is space in the inventory");

        yield return null;
    }
}