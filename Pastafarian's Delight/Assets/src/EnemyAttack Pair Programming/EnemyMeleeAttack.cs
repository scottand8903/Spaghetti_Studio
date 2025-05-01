using System;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackCooldown = 1f;
    public int damage = 1;
    private float lastAttackTime;


    public void Attack(Transform target)
    {
        if (Time.time < lastAttackTime + attackCooldown || target == null)
        {
            return;
        }
        // Attack logic here
        Debug.Log("Enemy performed melee attack!");

        HealthSystem.Instance.updateHealth(-damage);

        lastAttackTime = Time.time;
    }
}

