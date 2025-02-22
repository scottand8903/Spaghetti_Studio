using System.Threading;
using UnityEngine;
using static Enemy;

public class MeleeEnemy : Enemy
{
    

    // Update is called once per frame
    void Update()
    {
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
}
