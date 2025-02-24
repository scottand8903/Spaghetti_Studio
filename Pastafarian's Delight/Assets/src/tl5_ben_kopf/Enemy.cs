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
    [SerializeField] protected float changeDirectionTime = 3.0f;

    protected float speed;
    protected float health;
    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;

    protected Transform target;
    protected Rigidbody2D rb = null;

    public GameController gameController;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        if (gameController.GameRunning == true)
        {
            ChangeDirection();
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

