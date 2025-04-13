using UnityEngine;

public interface IPowerUpEffect
{
    void Apply(GameObject player);
}

// Base class with a virtual method (NOT derived from MonoBehaviour)
public class PowerUpEffectBase : IPowerUpEffect
{
    public virtual void Apply(GameObject player)
    {
        Debug.Log("Default Apply() from PowerUpEffectBase");
    }
}

// Derived class that uses override to change behavior dynamically
public class BasicHealthBoost : PowerUpEffectBase
{
    public override void Apply(GameObject player)
    {
        if (HealthSystem.Instance != null)
        {
            HealthSystem.Instance.updateHealth(1);
            Debug.Log("Overridden Apply() from BasicHealthBoost");
        }
        else
        {
            Debug.LogError("HealthSystem instance not found!");
        }
    }
}

// Decorator that logs the effect application
public class EffectLoggerDecorator : IPowerUpEffect
{
    private readonly IPowerUpEffect _baseEffect;

    public EffectLoggerDecorator(IPowerUpEffect baseEffect)
    {
        _baseEffect = baseEffect;
    }

    public void Apply(GameObject player)
    {
        Debug.Log("Logging effect use...");
        _baseEffect.Apply(player);
    }
}

// Decorator that calls the base effect multiple times based on a multiplier
public class HealthMultiplierDecorator : IPowerUpEffect
{
    private readonly IPowerUpEffect _baseEffect;
    private readonly int _multiplier;

    public HealthMultiplierDecorator(IPowerUpEffect baseEffect, int multiplier)
    {
        _baseEffect = baseEffect;
        _multiplier = multiplier;
    }

    public void Apply(GameObject player)
    {
        for (int i = 0; i < _multiplier; i++)
        {
            _baseEffect.Apply(player);
        }
    }
}
