using UnityEngine;
using UnityEngine.AI;
using System;
public class EnemyHandlerBC
{
    protected float health = 3.0f;
    protected float speed = 3.0f;
    
    public virtual float getHealth()
    {
        Debug.Log("Get Health called from super class");
        health = 0;
        return health;
    }
    public virtual void setHealth(float nhealth)
    {
        Debug.Log("Set Health called from super class");
        health = nhealth;
    }
    public virtual void updateHealth(float damage)
    {
        Debug.Log("Update Health called from super class");
        health += damage;
    }

}


public class EnemyHandler : EnemyHandlerBC
{
    public override float getHealth()
    {
        Debug.Log("Get Health called from sub class");
        return base.health;
    }
    public override void setHealth(float nhealth)
    {
        Debug.Log("Set Health called from sub class");
        base.health = nhealth;
    }
    public override void updateHealth(float damage)
    {
        Debug.Log("Update Health called from sub class");
        base.health += damage;
    }
}