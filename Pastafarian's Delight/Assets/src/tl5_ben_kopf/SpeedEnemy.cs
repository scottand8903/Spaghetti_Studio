using UnityEngine;

public class SpeedEnemy : MeleeEnemy
{
    //Overriding start to set speed health and move speed
    protected override void Start()
    {
        base.Start();
        updateEnemySpeed(2.5f);
        enemyhandler.setHealth(baseSpeedHealth);
        agent.stoppingDistance = 0f;

    }

}
