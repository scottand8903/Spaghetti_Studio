using UnityEngine;

public class HealthBoost : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // DYNAMIC BINDING
        IPowerUpEffect baseEffect = new BasicHealthBoost();
        effect = new EffectLoggerDecorator(new HealthMultiplierDecorator(baseEffect, 1));
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
