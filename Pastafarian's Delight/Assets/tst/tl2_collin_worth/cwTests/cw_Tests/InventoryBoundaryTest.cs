using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class InventorySystemTests
{
    private InventorySystem inventorySystem;
    private GameObject inventoryGameObject;
    private GameObject[] inventorySlots;
    private Sprite itemSprite;

    [SetUp]
    public void Setup()
    {
        // Create the game object for the inventory system
        inventoryGameObject = new GameObject();
        inventorySystem = inventoryGameObject.AddComponent<InventorySystem>();

        // Create mock inventory slots (can be empty GameObjects, or you can create UI Image objects if you prefer)
        inventorySlots = new GameObject[inventorySystem.inventorySlotsImages.Length];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = new GameObject("Slot " + i);
            inventorySlots[i].AddComponent<Image>(); // Simulate Image component for UI
        }

        // Create a test sprite for item to be added
        itemSprite = new GameObject("ItemSprite").AddComponent<SpriteRenderer>().sprite;
    }

    [UnityTest]
    public IEnumerator AddItem_ShouldFail_WhenInventoryIsFull()
    {
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
        // Add 3 items (less than max capacity)
        for (int i = 0; i < 3; i++)
        {
            int result = inventorySystem.addItem(i + 1, itemSprite);
            Assert.AreEqual(1, result, $"Item {i + 1} should be added successfully");
        }

        // Try to add another item when there's space
        int resultAfterAdding = inventorySystem.addItem(4, itemSprite);
        Assert.AreEqual(1, resultAfterAdding, "Item should be added successfully because there is space in the inventory");

        yield return null;
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(inventoryGameObject);
    }
}