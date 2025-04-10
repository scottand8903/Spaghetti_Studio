using UnityEngine;

public class SpeedEnemy : MeleeEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        updateEnemySpeed(2.5f);
        enemyhandler.setHealth(baseSpeedHealth);
        agent.stoppingDistance = 0f;

    }

    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
       // base.OnCollisionEnter2D(collision);
    //}
}
