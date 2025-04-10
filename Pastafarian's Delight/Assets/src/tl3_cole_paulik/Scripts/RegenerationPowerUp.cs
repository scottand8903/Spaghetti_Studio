using UnityEngine;

public class RegenerationPowerUp : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // Regeneration effect with 1 health per second for 5 seconds
        effect = new EffectLoggerDecorator(new RegenerationEffect(1f, 5f));
    }

    public override void ApplyEffect(GameObject player)
    {
        effect.Apply(player);
        Destroy(gameObject);  // Remove the power-up after use
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            effect.Apply(collision.gameObject);
            Destroy(gameObject);  // Remove the power-up after use
        }
    }
}
