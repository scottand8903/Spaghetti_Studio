using UnityEngine;

// Base MonoBehaviour class for power ups
public class PowerUp : MonoBehaviour
{
    public virtual void ApplyEffect(GameObject player)
    {
        Debug.Log("PowerUp applied to " + player.name);
    }
}

// MonoBehaviour that uses dynamic binding to select the correct health boost effect
public class HealthBoost : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // DYNAMIC BINDING: Although baseRef is of type PowerUpEffectBase,
        // it is assigned an instance of BasicHealthBoost, which overrides Apply().
        PowerUpEffectBase baseRef = new BasicHealthBoost();
        // Wrap the effect with decorators:
        effect = new EffectLoggerDecorator(new HealthMultiplierDecorator(baseRef, 1));
    }

    public override void ApplyEffect(GameObject player)
    {
        effect.Apply(player);
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            effect.Apply(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
