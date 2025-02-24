using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy;

public class MeleeEnemy : Enemy
{
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = baseHealth;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Idle movement when the enemy has no target
        if (target == null)
        {
            timer += Time.deltaTime;
            if(timer >= changeDirectionTime)
            {
                print("Changing Direction, from Melee");
                ChangeDirection();
                timer = 0f;
            }
            transform.Translate(moveDirection * speed *  Time.deltaTime, Space.World);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    //Collision Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.updateHealth(-1);
            updateEnemyHealth(-1);
        }
    }

}
