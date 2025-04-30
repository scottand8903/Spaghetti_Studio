using System;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackCooldown = 1f;
    public int damage = 10;
    private float lastAttackTime;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown || player == null) return;

        // Attack logic here
        Debug.Log("Enemy performed melee attack!");

        /*PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }*/ //Cant find player script to see what the health is called so placeholder for now

        lastAttackTime = Time.time;
    }
}

