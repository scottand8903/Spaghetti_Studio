using UnityEngine;
using System.Collections;

//public class SpikyTrap : MonoBehaviour //comment out for dynamic
public class SpikyTrap : Trap

{
    private bool isPlayerTouching = false;
    private Coroutine damageCoroutine;
    //comment in for dynamic: 
    public override void ActivateTrap()
    {
        Debug.Log("SpikyTrap activated!");
        // Specific SpikyTrap logic
    }


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