using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ItemSpawnerTestsp2
{
    private GameObject spawnerObj;
    private ItemSpawner spawner;

    [SetUp]
    public void Setup()
    {
        spawnerObj = new GameObject("Spawner");
        spawner = spawnerObj.AddComponent<ItemSpawner>();
        spawner.itemPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube); // dummy item
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(spawner.itemPrefab);
        Object.DestroyImmediate(spawnerObj);

        foreach (var obj in Object.FindObjectsOfType<GameObject>())
        {
            Object.DestroyImmediate(obj);
        }
    }

    [UnityTest]
    public IEnumerator SpawnItem_InsideBounds_SpawnsItem()
    {
        Vector2 position = new Vector2(5, 5);
        spawner.SpawnItem(position);

        yield return null;

        var spawned = GameObject.Find("Cube(Clone)");
        Assert.IsNotNull(spawned);
        Assert.AreEqual((Vector2)spawned.transform.position, position);
    }

    [UnityTest]
    public IEnumerator SpawnItem_OutsideBounds_DoesNotSpawnItem()
    {
        Vector2 position = new Vector2(9999, 9999); // simulate out of bounds
        spawner.bounds = new Rect(0, 0, 10, 10);

        spawner.SpawnItem(position);
        yield return null;

        var spawned = GameObject.Find("Cube(Clone)");
        Assert.IsNull(spawned);
    }

    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemPrefab;
        public Rect bounds = new Rect(0, 0, 1000, 1000); // default large area

        public void SpawnItem(Vector2 position)
        {
            if (!bounds.Contains(position))
            {
                Debug.Log("Out of bounds");
                return;
            }

            Instantiate(itemPrefab, position, Quaternion.identity);
        }
    }
}
