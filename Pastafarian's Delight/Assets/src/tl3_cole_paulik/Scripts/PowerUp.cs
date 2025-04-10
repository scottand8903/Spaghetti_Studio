using UnityEngine;

public interface IPowerUpEffect
{
    void Apply(GameObject player);
}

public class PowerUp : MonoBehaviour
{
    public virtual void ApplyEffect(GameObject player)
    {
        Debug.Log("PowerUp applied to " + player.name);
    }
}

// === Decorators and Core Effect Logic ===

public class BasicHealthBoost : IPowerUpEffect
{
    public virtual void Apply(GameObject player)
    {
        if (HealthSystem.Instance != null)
        {
            HealthSystem.Instance.updateHealth(1);
            Debug.Log("Basic Health Boost applied!");
        }
        else
        {
            Debug.LogError("HealthSystem instance not found!");
        }
    }
}

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
