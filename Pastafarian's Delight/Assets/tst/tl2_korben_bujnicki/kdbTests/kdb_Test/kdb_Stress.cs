using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StressTest
{
    [UnityTest]
    public IEnumerator Test_SpawnManyItems_Performance()
    {
        int itemCount = 10000000;

        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = new GameObject("Item" + i);
            item.AddComponent<Item>(); // Attach the Item script
            item.transform.position = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        }

        yield return null; // Let Unity process a frame

        int spawnedItems = GameObject.FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;

        Assert.GreaterOrEqual(spawnedItems, itemCount, "Not all items were instantiated correctly.");
    }
}
