using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LargeDamageTest
{
    private GameObject enemyGO;
    private Enemy enemy;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create a dummy enemy GameObject and add the Enemy component
        enemyGO = new GameObject("TestEnemy");
        enemyGO.tag = "Enemy"; // Needed for projectile detection

        enemy = enemyGO.AddComponent<Enemy>();
        enemyGO.AddComponent<Rigidbody2D>(); // So physics don't break

        // Ensure EnemyHandler is initialized
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator ProjectileDealsMassiveDamage()
    {
        // Arrange
        float largeDamage = -1000f;

        // Act
        enemy.TakeDamage(largeDamage);

        // Wait a bit for processing and destruction (just in case)
        yield return new WaitForSeconds(0.2f);

        // Assert
        Assert.IsTrue(enemy == null || enemyGO == null || enemyGO.Equals(null), "Enemy GameObject should be destroyed on massive damage.");
    }

    [TearDown]
    public void TearDown()
    {
        if (enemyGO != null)
            Object.Destroy(enemyGO);
    }
}
