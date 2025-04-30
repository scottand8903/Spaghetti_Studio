//Luke Halla: Pair Programming Scirpt
// This script is attached to a pickup object that increases the player's shoot rate when collected.
// This is stackable and allows for multiple pickups to be collected, each increasing the shoot rate further.

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShotRateBuffPickup : MonoBehaviour
{
    [Tooltip("Multiply the player's shotRate by this value (e.g. 2 = twice as fast).")]
    public float shotRateMultiplier = 2f;

    [Tooltip("The tag your player GameObject uses")]
    public string playerTag = "Player";

    void Awake()
    {
        // ensure this collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true; // ensure this collider is a trigger
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        // try to find Weapon on the player or in its children
        Weapon weapon = other.GetComponent<Weapon>()
                        ?? other.GetComponentInChildren<Weapon>(); // find in children

        if (weapon != null)
        {
            float oldRate = weapon.shotRate;
            weapon.shotRate = oldRate * shotRateMultiplier; // increase the shot rate
            Debug.Log($"[ShotRateBuffPickup] shotRate {oldRate} → {weapon.shotRate}"); 
        }
        else
        {
            Debug.LogWarning($"[ShotRateBuffPickup] no Weapon component found on '{other.name}'.");
        }

        // consume the pickup
        Destroy(gameObject);
    }
}
