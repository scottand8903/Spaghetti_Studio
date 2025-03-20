using UnityEngine;

public class SpeedBoost : PowerUp
{
    public float speedIncrease = 2f;
    public float duration = 5f;

    public override void ApplyEffect(GameObject player)
    {
        /*PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.StartCoroutine(controller.IncreaseSpeed(speedIncrease, duration));
        }*/
        Destroy(gameObject); // Remove power-up after use
    }
}
