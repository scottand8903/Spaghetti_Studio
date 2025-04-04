using UnityEngine;

public class HealthBoost : PowerUp
{
    public int healthIncrease = 1;  // Default value

    public override void ApplyEffect(GameObject player)
    {
        if (HealthSystem.Instance != null) 
        {
            HealthSystem.Instance.updateHealth(healthIncrease);  // Call the existing updateHealth method
            Debug.Log("Health Boost applied! Increased by " + healthIncrease);
        }
        else
        {
            Debug.LogError("HealthSystem instance not found!");
        }

        Destroy(gameObject);  // Remove the power-up after use
    }
}
