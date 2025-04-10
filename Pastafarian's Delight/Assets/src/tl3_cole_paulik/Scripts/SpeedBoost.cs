using UnityEngine;
using System.Collections;

public class SpeedBoost : PowerUp
{
    private IPowerUpEffect effect;

    private void Awake()
    {
        // Build the decorator chain for speed boost
        IPowerUpEffect baseEffect = new BasicSpeedBoost(5f, 10f);
        effect = new EffectLoggerDecorator(new DurationExtenderDecorator(baseEffect, 5f));
    }

    public override void ApplyEffect(GameObject player)
    {
        effect.Apply(player);
        Destroy(gameObject); // Remove power-up after use
    }

    private class BasicSpeedBoost : IPowerUpEffect
    {
        private float speedIncrease;
        private float duration;

        public BasicSpeedBoost(float speedIncrease, float duration)
        {
            this.speedIncrease = speedIncrease;
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            PlayerController playerController = PlayerController.Instance;
            if (playerController == null)
            {
                Debug.LogError("PlayerController instance not found!");
                return;
            }

            playerController.StartCoroutine(ApplySpeedBoost(playerController));
        }

        private IEnumerator ApplySpeedBoost(PlayerController controller)
        {
            var moveSpeedField = typeof(PlayerController).GetField("moveSpeed",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (moveSpeedField == null)
            {
                Debug.LogError("moveSpeed field not accessible");
                yield break;
            }

            float originalSpeed = (float)moveSpeedField.GetValue(controller);
            float boostedSpeed = originalSpeed + speedIncrease;
            moveSpeedField.SetValue(controller, boostedSpeed);

            Debug.Log("Speed Boost Applied! New Speed: " + boostedSpeed);
            yield return new WaitForSeconds(duration);
            moveSpeedField.SetValue(controller, originalSpeed);
            Debug.Log("Speed Boost Ended. Speed reset to: " + originalSpeed);
        }
    }

    private class DurationExtenderDecorator : IPowerUpEffect
    {
        private readonly IPowerUpEffect baseEffect;
        private readonly float extraDuration;

        public DurationExtenderDecorator(IPowerUpEffect baseEffect, float extraDuration)
        {
            this.baseEffect = baseEffect;
            this.extraDuration = extraDuration;
        }

        public void Apply(GameObject player)
        {
            if (baseEffect is BasicSpeedBoost baseSpeedBoost)
            {
                IPowerUpEffect boosted = new BasicSpeedBoost(5f, 10f + extraDuration);
                boosted.Apply(player);
            }
            else
            {
                baseEffect.Apply(player);
            }
        }
    }

    private class EffectLoggerDecorator : IPowerUpEffect
    {
        private readonly IPowerUpEffect baseEffect;

        public EffectLoggerDecorator(IPowerUpEffect baseEffect)
        {
            this.baseEffect = baseEffect;
        }

        public void Apply(GameObject player)
        {
            Debug.Log("[EffectLogger] Applying speed boost to " + player.name);
            baseEffect.Apply(player);
            Debug.Log("[EffectLogger] Speed boost applied.");
        }
    }
}
