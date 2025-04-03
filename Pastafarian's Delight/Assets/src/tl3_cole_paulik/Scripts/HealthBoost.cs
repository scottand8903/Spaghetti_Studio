using UnityEngine;

public class HealthBoost : PowerUp
{
    public int healthIncrease = 1;  // Default value

    public override void ApplyEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            HealthSystem.Instance.updateHealth(healthIncrease);  // Call the existing updateHealth method
            Debug.Log("Health Boost applied! Increased by " + healthIncrease);
        }

        Destroy(gameObject);  // Remove the power-up after use
    }
}
