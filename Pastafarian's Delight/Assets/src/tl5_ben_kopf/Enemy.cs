using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float health = 10.0f;
    [SerializeField] protected float viewDistance = 15.0f;
    [SerializeField] protected string enemyType = "None";
    [SerializeField] protected float changeDirectionTime = 3.0f;

    protected Vector2 moveDirection;
    protected Vector2 originPos;
    protected Vector2 currentPos;
    protected float timer;

    protected Transform target;
    protected Rigidbody2D rb = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    protected void ChangeDirection()
    {
        print("getting new direction from ENEMY");
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

}

