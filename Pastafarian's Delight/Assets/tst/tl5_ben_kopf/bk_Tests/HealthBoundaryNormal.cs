using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class HealthBoundaryMelee
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
    public IEnumerator HealthBoundaryMeleeWithEnumeratorPasses()
    {
        var enemy = GameObject.FindObjectOfType<MeleeEnemy>();
        Assert.IsNotNull(enemy, "Enemy Not found in this scene");

        while (enemy.enemyhandler.getHealth() > 1)
        {
            enemy.enemyhandler.updateHealth(-1);
            Debug.Log(enemy.enemyhandler.getHealth());
            yield return null;
        }
        Assert.AreEqual(1, enemy.enemyhandler.getHealth());
        enemy.enemyhandler.updateHealth(float.MinValue);
        yield return null;
        Assert.False(enemy);
        yield return null;
    }
}
