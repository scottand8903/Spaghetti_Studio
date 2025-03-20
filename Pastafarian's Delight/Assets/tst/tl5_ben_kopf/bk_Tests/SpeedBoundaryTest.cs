using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SpeedBoundaryTest
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
    public IEnumerator SpeedBoundaryTestWithEnumeratorPasses()
    {
        var enemy = GameObject.FindObjectOfType<MeleeEnemy>();
        Assert.IsNotNull(enemy, "Enemy Not found in this scene");

        while (enemy.getSpeed() > 1)
        {
            enemy.updateEnemySpeed(-1.0f);
            yield return null;
        }
        Assert.AreEqual(1, enemy.getSpeed());
        enemy.updateEnemySpeed(-1.0f);
        yield return null;
        Assert.AreNotEqual(0.0f, enemy.getSpeed());
        Assert.AreEqual(1.0f,enemy.getSpeed());
        enemy.updateEnemySpeed(float.MaxValue);
        yield return null;
        Assert.AreEqual(100, enemy.getSpeed());
    }
}
