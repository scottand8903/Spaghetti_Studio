using Mono.Cecil;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Enemy;

public class MeleeEnemy : Enemy
{
    private MeleeAttack meleeAttack;
    protected override void Start()
    {
        base.Start();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    // Update is called once per frame
    private void Update()
	{
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        if (player != null)
		{
			float distanceToPlayer = Vector2.Distance(rb.position, player.position);
			if (distanceToPlayer <= agroRange)
			{
				target = player;
			}
			if (distanceToPlayer >= viewDistance)
			{
				target = null;
			}
		}
		if (target == null)
		{
			Wander(timer, wanderTimer, wanderRadius, agent);
		}
		if(target != null)
        {
            MeleeMove(target, agent);
            if (Vector2.Distance(transform.position, player.position) <= stopDistance)
            {
                meleeAttack?.Attack(); // Only call Attack when in range
            }
        }
    }

	void MeleeMove(Transform target,NavMeshAgent agent)
	{
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true; // Stop moving when close enough
        }
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
    //Collision Damage
    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
    //    base.OnCollisionEnter2D(collision);
    //}
}
