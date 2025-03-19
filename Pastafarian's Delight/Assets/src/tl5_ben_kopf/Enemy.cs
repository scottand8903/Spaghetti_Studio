using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected float baseSpeed = 3.0f;
    //[SerializeField] private float baseSpeedSpeed = 4.5f; //Speed of the fast enemies
    //[SerializeField] private float baseTankSpeed = 1.5f;  //Speed of the tank enemies
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
	//public GameObject gameController;
	//public event Action OnDeath;

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
    //protected GameObject this_enemy;


    //protected void ChangeDirection()
    //{
    //   print("getting new direction from ENEMY");
    //   float angle = Random.Range(0f, 360f);
    //   moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    //}

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        health = baseHealth;
        agent.speed = baseSpeed;
        timer = wanderTimer;
        findPlayer();
        //Debug.Log(health);
    }

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
        if (collision.gameObject.CompareTag("Player") && Time.time > lastHitTime + hitCooldown)
        {
            Debug.Log(gameObject.name + " Player Hit " + lastHitTime);
            //gameController.updateHealth(-1);
            lastHitTime = Time.time;
            updateEnemyHealth(-1);
        }

        // Check if the enemy collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("Wall Hit");
            // Stop movement by zeroing the Rigidbody velocity
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void updateEnemyHealth(int damage)
    {
		Debug.Log(gameObject.name + "Health Before" + health);
        health += damage;
		Debug.Log(gameObject.name + "Health After" + health);
        if(health <= 0)
        {
			Die();
        }
    }

	private void Die()
	{
        Debug.Log(gameObject.name + "Died with " + health + " health");
		//add animator stuff here when that happens
		Destroy(gameObject);
	}
}


