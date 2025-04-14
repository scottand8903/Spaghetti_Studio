using UnityEngine;

// Base MonoBehaviour class for power ups
public class PowerUp : MonoBehaviour
{
    // Virtual method to apply the effect to the player
    public virtual void ApplyEffect(GameObject player)
    {
        Debug.Log("PowerUp applied to " + player.name);
    }
}

// Health boost power-up using decorators and dynamic binding
public class HealthBoost : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // Create base effect and wrap it in decorators
        PowerUpEffectBase baseRef = new BasicHealthBoost();
        effect = new EffectLoggerDecorator(new HealthMultiplierDecorator(baseRef, 1));
    }

    // Override the ApplyEffect method
    public override void ApplyEffect(GameObject player)
    {
        effect.Apply(player);
        Destroy(gameObject);
    }

    // Trigger effect when colliding with player
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            effect.Apply(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
