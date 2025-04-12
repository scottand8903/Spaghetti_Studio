using UnityEngine;

public class TankEnemy : MeleeEnemy
{
    
    // Overriding start to set tanks health
    protected override void Start()
    {
        base.Start();
        enemyhandler.setHealth(baseTankHealth);

    }

}
