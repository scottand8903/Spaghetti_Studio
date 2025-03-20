using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class ItemSpawnerTests
{
    private GameObject spawnerObject;
    private ItemSpawner itemSpawner;

    [SetUp]
    public void Setup()
    {
        spawnerObject = new GameObject();
        itemSpawner = spawnerObject.AddComponent<ItemSpawner>();
        itemSpawner.itemPrefab = new GameObject(); // Mock prefab
    }

    [UnityTest]
    public IEnumerator SpawnItems_WithinBounds_SpawnsSuccessfully()
    {
        itemSpawner.SpawnItems(999);

        yield return null; // Wait a frame

        Assert.AreEqual(999, itemSpawner.transform.childCount, "Expected 999 items to be spawned.");
    }

    [UnityTest]
    public IEnumerator SpawnItems_ExceedsLimit_StopsAtMax()
    {
        itemSpawner.limitEnabled = true; // Ensure limit is applied
        itemSpawner.SpawnItems(1001);

        yield return null; // Wait a frame

        Assert.AreEqual(1000, itemSpawner.transform.childCount, "Spawner should not exceed 1000 items.");
    }


    [TearDown]
    public void Teardown()
    {
        Object.Destroy(spawnerObject);
    }
}

    
