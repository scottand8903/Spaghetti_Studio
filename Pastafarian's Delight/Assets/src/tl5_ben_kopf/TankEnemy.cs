using UnityEngine;

public class TankEnemy : MeleeEnemy
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyhandler.setHealth(baseTankHealth);
        Debug.Log(gameObject.name + " spawned with " + health + " health.");

    }

    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
     //   base.OnCollisionEnter2D(collision);
    //}
}
