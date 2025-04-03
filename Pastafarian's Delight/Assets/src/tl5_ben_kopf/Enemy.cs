using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected float baseSpeed = 3.0f;
    [SerializeField] protected float baseHealth = 3.0f;
    [SerializeField] protected float baseSpeedHealth = 1.5f; //Health of the fast enemies
    [SerializeField] protected float baseTankHealth = 5.0f; // Health of the tank enemies
    [SerializeField] protected float viewDistance = 15.0f;
    [SerializeField] protected float agroRange = 5.0f;
    [SerializeField] protected string enemyType = "None";
    [SerializeField] protected float wanderTimer = 3.0f;
    [SerializeField] protected float wanderRadius = 10f;
    [SerializeField] protected float hitCooldown = 1f;
    public Transform player;
    public EnemyHandlerBC enemyhandler;

    protected float speed;
    protected float health;
    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;
    

    protected Transform target = null;
    protected Rigidbody2D rb = null;
    protected NavMeshAgent agent;
    private float lastHitTime = 0f;


    //contains all basic start information needed for enemies
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        agent.speed = baseSpeed;
        timer = wanderTimer;
        findPlayer();
        enemyhandler = new EnemyHandler();
        enemyhandler.setHealth(baseHealth);
    }


    //sets the player trasform to the player's position
    protected void findPlayer()
	{
		if (player == null)
		{
			GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
			if (playerObj != null)
			{
				player = playerObj.transform;
			}
		}
	}


    //General enemy movement, allows them to wander while they don't have a target
	protected void Wander(float timer, float wanderTimer, float wanderRadius, NavMeshAgent agent)
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer)
		{
			// Generate a random point within a circle
			Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * wanderRadius;
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


    //collision damage
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Ouch");
        //Detects player collision
        if (collision.gameObject.CompareTag("Player") && Time.time > lastHitTime + hitCooldown)
        {
			lastHitTime = Time.time; //Tracks the last time the player was hit
			Debug.Log(gameObject.name + " Player Hit " + lastHitTime);
            HealthSystem.Instance.updateHealth(-1); //Damages player
            enemyhandler.updateHealth(-1); // Damages enemy
            Debug.Log(enemyhandler.getHealth());
            if (enemyhandler.getHealth() <= 0)
            {
                Die();
            }
        }

        // Check if the enemy collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("Wall Hit");
            // Stop movement by zeroing the Rigidbody velocity
            rb.linearVelocity = Vector2.zero;
        }
    }

    //Returns the speed of an enemy
    public float getSpeed()
    {
        return agent.speed;
    }


    //Updates Enemy speed
    public void updateEnemySpeed(float speed)
    {
        agent.speed += speed;
        if (agent.speed <= 0)
        {
            agent.speed = 1;
        }
        if (agent.speed > 100000)
        {
            agent.speed = 100;
        }
    }

    public void TakeDamage(float damage)
    {
        enemyhandler.updateHealth(damage);
        if(enemyhandler.getHealth() <= 0)
        {
            Die();
        }
    }
    protected void Die()
    {
        Debug.Log(gameObject.name + "Died with " + health + " health");
        //add animator stuff here when that happens
        Destroy(gameObject);
    }

}


