using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Enemy;

public class MeleeEnemy : Enemy
{
    private void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
		rb = GetComponent<Rigidbody2D>();
		health = baseHealth;
        speed = baseSpeed;
        timer = wanderTimer;
		findPlayer();
	}

	// Update is called once per frame
	private void Update()
	{
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
}
