using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoundaryTest_NoInventorySystem
{
    private GameObject itemObject;

    [SetUp]
    public void Setup()
    {
        itemObject = new GameObject("Item", typeof(Item));
    }

    [UnityTest]
    public IEnumerator Test_NoInventorySystem_PickupFails()
    {
        Item item = itemObject.GetComponent<Item>();

        LogAssert.Expect(LogType.Error, "InventorySystem not found!");

        item.TryPickUp();

        yield return null;
    }
}

