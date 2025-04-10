using UnityEngine;

public class SpeedEnemy : MeleeEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        agent.speed *= 2f;
        enemyhandler.setHealth(baseSpeedHealth);
        Debug.Log(gameObject.name + " spawned with " + health + " health.");
        agent.stoppingDistance = 0f;

    }

    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
       // base.OnCollisionEnter2D(collision);
    //}
}
