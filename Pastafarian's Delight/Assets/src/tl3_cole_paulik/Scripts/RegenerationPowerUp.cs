using UnityEngine;

// PowerUp that gives regeneration effect
public class RegenerationPowerUp : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // Wrap regeneration effect in a logger decorator
        effect = new EffectLoggerDecorator(new RegenerationEffect(1f, 5f));
    }

    public override void ApplyEffect(GameObject player)
    {
        effect.Apply(player);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            effect.Apply(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
