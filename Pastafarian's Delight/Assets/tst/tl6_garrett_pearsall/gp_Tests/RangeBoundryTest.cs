using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MeleeAttackRangeTest
{
    private GameObject meleeObject;
    private MeleeAttack meleeAttack;

    private GameObject enemyInside;
    private GameObject enemyOutside;

    private class MockEnemy : MonoBehaviour
    {
        public int health = 1;

        public void TakeDamage(int damage)
        {
            health -= damage;
        }
    }

    [UnityTest]
    public IEnumerator EnemiesOnlyWithinRangeShouldTakeDamage()
    {
        // Setup melee attack object
        meleeObject = new GameObject("MeleeAttackObject");
        meleeAttack = meleeObject.AddComponent<MeleeAttack>();
        Transform attackPoint = new GameObject("AttackPoint").transform;
        attackPoint.position = Vector3.zero; // center point
        attackPoint.SetParent(meleeObject.transform);
        meleeAttack.attackPoint = attackPoint;
        meleeAttack.attackRange = 1.0f;
        meleeAttack.enemyLayers = LayerMask.GetMask("Enemy");

        // Setup enemy inside range
        enemyInside = new GameObject("EnemyInside");
        enemyInside.transform.position = new Vector3(0.5f, 0, 0); // Within 1.0 range
        enemyInside.AddComponent<CircleCollider2D>();
        enemyInside.AddComponent<MockEnemy>();
        enemyInside.layer = LayerMask.NameToLayer("Enemy");

        // Setup enemy outside range
        enemyOutside = new GameObject("EnemyOutside");
        enemyOutside.transform.position = new Vector3(2.0f, 0, 0); // Outside 1.0 range
        enemyOutside.AddComponent<CircleCollider2D>();
        enemyOutside.AddComponent<MockEnemy>();
        enemyOutside.layer = LayerMask.NameToLayer("Enemy");

        // Wait one frame to let physics update
        yield return null;

        // Perform attack
        meleeAttack.Attack();

        // Verify: Inside enemy was damaged, outside was not
        int healthInside = enemyInside.GetComponent<MockEnemy>().health;
        int healthOutside = enemyOutside.GetComponent<MockEnemy>().health;

        Assert.Less(healthInside, 1, "Enemy inside range should take damage.");
        Assert.AreEqual(1, healthOutside, "Enemy outside range should not take damage.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(meleeObject);
        Object.Destroy(enemyInside);
        Object.Destroy(enemyOutside);
    }
}
