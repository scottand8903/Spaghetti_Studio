using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Regeneration effect that restores health over time
public class RegenerationEffect : IPowerUpEffect
{
    private float regenerationAmount;
    private float duration;
    private float elapsedTime;

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
            healthSystem.updateHealth((int)regenerationAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Regeneration effect ended.");
    }
}

