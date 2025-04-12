using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected float baseSpeed = 3.0f; //Speed for base class of enemies
    [SerializeField] protected float baseHealth = 3.0f; //Health for base class of enemies
    [SerializeField] protected float baseSpeedHealth = 1.5f; //Health of the fast enemies
    [SerializeField] protected float baseTankHealth = 5.0f; // Health of the tank enemies
    [SerializeField] protected float viewDistance = 15.0f; //How far an enemy will chase once player is targeted
    [SerializeField] protected float agroRange = 5.0f; //Once within this range the player will become the targer
    [SerializeField] protected string enemyType = "None";
    [SerializeField] protected float wanderTimer = 3.0f; //Interval between change in wander points
    [SerializeField] protected float wanderRadius = 10f; //Radius of enemy wander
    [SerializeField] protected float hitCooldown = 1f; //Time between collision hits
    public Transform player; //Player location
    public EnemyHandlerBC enemyhandler; //Manages enemy health
    public float stopDistance = 2f; //Distance away from the player enemies stop

    protected float speed;
    protected float health;
    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;
    

    protected Transform target = null; //Target for aggro range
    protected Rigidbody2D rb = null;
    protected NavMeshAgent agent;
    private float lastHitTime = 0f;


    //contains all basic start information needed for enemies
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //Keeps the enemy from roatating
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        agent.speed = baseSpeed;
        timer = wanderTimer;
        findPlayer();
        enemyhandler = new EnemyHandler(); //Makes a new enemy handler
        enemyhandler.setHealth(baseHealth);
        enemyhandler.OnDeath += Die; //Subscribing to the enemyhandler OnDeath event with the Die function
        agent.stoppingDistance = stopDistance;
    }


    //sets the player transform to the player's position
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

    //Public funciton for the enemy to take damage
    public void TakeDamage(float damage)
    {
        enemyhandler.updateHealth(damage);
        if(enemyhandler.getHealth() <= 0)
        {
            Die();
        }
    }

    //Destroys the game object when called
    protected void Die()
    {
        Debug.Log(gameObject.name + "Died with " + health + " health");
        //add animator stuff here when that happens
        Destroy(gameObject);
    }
	private void OnDestroy()
	{
        enemyhandler.OnDeath -= Die; //Unsubscribe from the event
	}
}


