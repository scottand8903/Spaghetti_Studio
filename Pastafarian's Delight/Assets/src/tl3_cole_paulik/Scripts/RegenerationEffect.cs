using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationEffect : IPowerUpEffect
{
    private float regenerationAmount;  // Amount of health to regenerate per second
    private float duration;            // Duration of the regeneration effect
    private float elapsedTime;         // Time elapsed since the effect started

    public RegenerationEffect(float regenerationAmount, float duration)
    {
        this.regenerationAmount = regenerationAmount;
        this.duration = duration;
        this.elapsedTime = 0f;
    }

    public void Apply(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on " + player.name);
            return;
        }

        playerController.StartCoroutine(ApplyRegeneration(player));
    }

    private IEnumerator ApplyRegeneration(GameObject player)
    {
        HealthSystem healthSystem = HealthSystem.Instance;
        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem instance not found!");
            yield break;
        }

        while (elapsedTime < duration)
        {
            healthSystem.updateHealth((int)regenerationAmount);  // Regenerate health
            elapsedTime += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait for the next frame
        }
        Debug.Log("Regeneration effect ended.");
    }
}

