using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected float baseSpeed = 3.0f;
    //[SerializeField] private float baseSpeedSpeed = 4.5f; //Speed of the fast enemies
    //[SerializeField] private float baseTankSpeed = 1.5f;  //Speed of the tank enemies
    [SerializeField] protected float baseHealth = 3.0f;
   //[SerializeField] protected float baseSpeedHealth = 1.5f; //Health of the fast enemies
    //[SerializeField] protected float baseTankHealth = 5.0f; // Health of the tank enemies
    [SerializeField] protected float viewDistance = 15.0f;
    [SerializeField] protected float agroRange = 5.0f;
    [SerializeField] protected string enemyType = "None";
    [SerializeField] protected float wanderTimer = 3.0f;
    [SerializeField] protected float wanderRadius = 10f;
    public Transform player;

    protected float speed;
    protected float health;
    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;

    protected Transform target = null;
    protected Rigidbody2D rb = null;
    protected NavMeshAgent agent;

    protected GameController gameController = null;
    

	//protected void ChangeDirection()
	//{
	//   print("getting new direction from ENEMY");
	//   float angle = Random.Range(0f, 360f);
	//   moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
	//}

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



	public void updateEnemyHealth(int damage)
    {
        health += damage;
        if(health < 1)
        {
            Destroy(rb);
        }
    }
}

