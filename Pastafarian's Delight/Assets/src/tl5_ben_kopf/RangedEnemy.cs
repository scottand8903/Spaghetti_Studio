using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Enemy;

public class RangedEnemy : Enemy
{
    [SerializeField] private float maxDistance = 5.0f; // Distance at which enemy stops moving
    [SerializeField] private float tooClose = 3.0f;
    [SerializeField] private float attackRange = 5.0f;

    private RangedAttack rangedAttack;
    protected override void Start()
    {
        base.Start();
        agent.stoppingDistance = 0f;
        rangedAttack = GetComponent<RangedAttack>();
    }

    private void Update()
    {
        //Handles the ranged enemies wander movment and player position
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
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                rangedAttack.TryFireAt(target);
            }
            RangedMovement(target, agent);
            
        }
    }
    

    //Ranged enemy movment logic once a target is acquired
    private void RangedMovement(Transform target, NavMeshAgent agent)
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);


        if (distanceToTarget > maxDistance) // Move only if beyond stop distance
        {
            agent.SetDestination(target.position);
        }
        else if (distanceToTarget <= tooClose)
        {
            // Move away from the player
            Vector2 retreatDirection = (transform.position - target.position).normalized;
            Vector3 retreatPosition = transform.position + (Vector3)retreatDirection * maxDistance;

            // Ensure the enemy can move to the new position
            NavMeshHit hit;
            if (NavMesh.SamplePosition(retreatPosition, out hit, maxDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        {
            agent.ResetPath(); // Stop movement when close enough
        }
    }

}
