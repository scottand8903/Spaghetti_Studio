using UnityEngine;
using UnityEngine.AI;
using System;

//Super class
public class EnemyHandlerBC
{
    protected float health = 3.0f;
    protected float speed = 3.0f;
	public event Action OnDeath;

    //Sets health to 1 so enemies are easier to kill in BC mode
	public virtual float getHealth()
    {
        Debug.Log("Get Health called from super class");
        health = 1;
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
	protected void Die()
	{
		OnDeath?.Invoke();
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
        Debug.Log("Update Health called from sub class:");
        base.health += damage;
        if(base.health <= 0)
        {
            Die();
        }
    }
}