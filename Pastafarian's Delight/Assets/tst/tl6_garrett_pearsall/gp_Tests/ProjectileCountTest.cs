using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ProjectileStressTest
{
    private GameObject projectilePrefab;
    private int projectileCount = 1000;
    private GameObject[] spawnedProjectiles;

    [SetUp]
    public void SetUp()
    {
        // Create a simple projectile prefab
        projectilePrefab = new GameObject("Projectile");
        projectilePrefab.AddComponent<Rigidbody2D>(); // Optional for physics
        projectilePrefab.AddComponent<BoxCollider2D>();
        projectilePrefab.AddComponent<Projectile>();
    }

    [UnityTest]
    public IEnumerator SpawnManyProjectiles_ShouldNotCrash()
    {
        spawnedProjectiles = new GameObject[projectileCount];

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPosition = new Vector3(i * 0.1f, 0, 0); // Spread them out
            spawnedProjectiles[i] = GameObject.Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.5f); // Let the game run a few frames

        var remainingProjectiles = GameObject.FindObjectsOfType<Projectile>().Length;
        Debug.Log($"Active projectiles: {remainingProjectiles}");

        Assert.AreEqual(projectileCount, remainingProjectiles, "Not all projectiles were spawned or survived.");
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {
            GameObject.Destroy(obj);
        }
    }
}
