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
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        // try to find Weapon on the player or in its children
        Weapon weapon = other.GetComponent<Weapon>()
                        ?? other.GetComponentInChildren<Weapon>();

        if (weapon != null)
        {
            float oldRate = weapon.shotRate;
            weapon.shotRate = oldRate * shotRateMultiplier;
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
