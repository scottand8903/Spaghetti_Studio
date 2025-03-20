using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class HealthBoundaryTest
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("EnemyTests");
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthBoundaryTestWithEnumeratorPasses()
    {
        var enemy = GameObject.FindObjectOfType<MeleeEnemy>();
        Assert.IsNotNull(enemy, "Enemy Not found in this scene");

        while (enemy.getHealth() > 1)
        {
            enemy.updateEnemyHealth(-1);
            yield return null;
        }
        Assert.AreEqual(1, enemy.getHealth());
        enemy.updateEnemyHealth(float.MinValue);
        yield return null;
        Assert.False(enemy);
        yield return null;
    }
}
