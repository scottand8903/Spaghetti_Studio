using UnityEngine;
using System.Collections;

public class SpikyTrap : MonoBehaviour
{
    private bool isPlayerTouching = false;
    private Coroutine damageCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.updateHealth(-1); // Instant damage
                isPlayerTouching = true;
                damageCoroutine = StartCoroutine(DealDamageOverTime());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DealDamageOverTime()
    {
        while (isPlayerTouching)
        {
            yield return new WaitForSeconds(1f);
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.updateHealth(-1);
            }
        }
    }

}