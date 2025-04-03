using UnityEngine;
using System.Collections;

public class SpeedBoost : PowerUp
{
    public float speedIncrease = 5f; // Amount to increase speed
    public float duration = 10f; // Duration of speed boost
    private static bool isSpeedBoostActive = false; // Prevents multiple instances

    public override void ApplyEffect(GameObject player)
    {
        if (PlayerController.Instance != null)
        {
            if (!isSpeedBoostActive) // Apply only if no active boost
            {
                PlayerController.Instance.StartCoroutine(ApplySpeedBoost());
            }
        }
        else
        {
            Debug.LogError("PlayerController instance not found!");
        }

        Destroy(gameObject); // Remove power-up after use
    }

    private IEnumerator ApplySpeedBoost()
    {
        var playerController = PlayerController.Instance;
        if (playerController == null) yield break;

        var moveSpeedField = typeof(PlayerController).GetField("moveSpeed",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (moveSpeedField == null)
        {
            Debug.LogError("Failed to access moveSpeed field.");
            yield break;
        }

        if (isSpeedBoostActive) yield break; // Prevent stacking
        isSpeedBoostActive = true;

        float originalSpeed = (float)moveSpeedField.GetValue(playerController);
        float boostedSpeed = originalSpeed + speedIncrease;

        // Apply speed boost
        moveSpeedField.SetValue(playerController, boostedSpeed);
        Debug.Log("Speed Boost Applied! New Speed: " + boostedSpeed);

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Reset speed
        moveSpeedField.SetValue(playerController, originalSpeed);
        Debug.Log("Speed Boost Ended. Speed reset to: " + originalSpeed);

        isSpeedBoostActive = false; // Allow new boosts after reset
    }
}
