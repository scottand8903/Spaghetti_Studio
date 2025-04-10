using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class HealthBoundaryRanged
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
	public IEnumerator HealthBoundaryRangedWithEnumeratorPasses()
	{
		var enemy = GameObject.FindObjectOfType<RangedEnemy>();
		Assert.IsNotNull(enemy, "Enemy Not found in this scene");

		while (enemy.enemyhandler.getHealth() > 1)
		{
			enemy.TakeDamage(-1);
			yield return null;
		}
		Assert.AreEqual(1, enemy.enemyhandler.getHealth());
		enemy.TakeDamage(float.MinValue);
		yield return null;
		Assert.False(enemy);
		yield return null;
	}
}

