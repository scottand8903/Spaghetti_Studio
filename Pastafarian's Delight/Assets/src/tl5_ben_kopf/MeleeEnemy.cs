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
        rb = GetComponent<Rigidbody2D>();
        health = baseHealth;
        speed = baseSpeed;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        
            //Idle movement when the enemy has no target
            if (target == null)
            {
                timer += Time.deltaTime;

                if (timer >= wanderTimer)
                {
                    // Generate a random point within a circle
                    Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
                    // Convert the 2D random point to a 3D destination (Z remains 0)
                    Vector3 destination = new Vector3(randomPoint.x, randomPoint.y, 0f) + transform.position;

                    // Validate that the destination is on the NavMesh
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(destination, out hit, wanderRadius, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                    timer = 0f;
                }
            }
      
    }


    //Collision Damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.updateHealth(-1);
            updateEnemyHealth(-1);
        }

        // Check if the enemy collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop movement by zeroing the Rigidbody velocity
            rb.linearVelocity = Vector2.zero;
        }
    }

}
