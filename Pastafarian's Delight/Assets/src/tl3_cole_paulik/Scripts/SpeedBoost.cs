/*using UnityEngine;

public class SpeedBoost : PowerUp
{
    private float speedIncrease;
    private float duration;
    
    public SpeedBoost(float speed, float time)
    {
        speedIncrease = speed;
        duration = time;
    }
    
    public override void ApplyEffect(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.StartCoroutine(controller.IncreaseSpeed(speedIncrease, duration));
        }
    }
}*/