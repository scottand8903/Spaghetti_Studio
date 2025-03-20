using UnityEngine;

public class HealthBoost : PowerUp
{
    public int healthIncrease = 20;

    public override void ApplyEffect(GameObject player)
    {
        /*PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healthIncrease);
        }*/
        Destroy(gameObject); // Remove power-up after use
    }
}
