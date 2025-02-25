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
    [SerializeField] protected string enemyType = "None";
    [SerializeField] protected float wanderTimer = 3.0f;
    [SerializeField] protected float wanderRadius = 10f;

    protected float speed;
    protected float health;
    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;

    protected Transform target;
    protected Rigidbody2D rb = null;
    protected NavMeshAgent agent;

    protected GameController gameController = null;
    

    private void Start()
    {
        GameObject controllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (controllerObject != null)
        {
            gameController = controllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.LogError("GameController GameObject with tag not found");
        }
    }

    protected void ChangeDirection()
    {
        print("getting new direction from ENEMY");
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
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

