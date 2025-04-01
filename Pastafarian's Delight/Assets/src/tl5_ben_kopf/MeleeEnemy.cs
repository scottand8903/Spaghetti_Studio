using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Enemy;

public class MeleeEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
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
			MeleeMove(target,agent);
		}
	}

	void MeleeMove(Transform target,NavMeshAgent agent)
	{
		agent.SetDestination((Vector3)target.position);
	}
    //Collision Damage
    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
    //    base.OnCollisionEnter2D(collision);
    //}
}
