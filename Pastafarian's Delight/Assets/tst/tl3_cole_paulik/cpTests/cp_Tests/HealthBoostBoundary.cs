using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class HealthBoostTests
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private GameObject healthBoostItem;
    private HealthBoost healthBoostScript;

    [SetUp]
    public void Setup()
    {
        // Setup Player GameObject with PlayerHealth
        player = new GameObject("Player");
        playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.maxHealth = 100f;
        playerHealth.currentHealth = 50f;

        // Setup HealthBoost item
        healthBoostItem = new GameObject("HealthBoost");
        healthBoostScript = healthBoostItem.AddComponent<HealthBoost>();
        healthBoostScript.boostAmount = 20f; // Boosts health by 20

        // Add collider for detection
        healthBoostItem.AddComponent<BoxCollider2D>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(healthBoostItem);
    }

    [UnityTest]
    public IEnumerator HealthBoost_InsideBounds_IncreasesHealth()
    {
        Vector2 position = new Vector2(5, 5);
        healthBoostItem.transform.position = position;
        
        // Simulate picking up the health boost item
        healthBoostScript.OnPickup(playerHealth);
        yield return null;

        Assert.AreEqual(70f, playerHealth.currentHealth); // 50 + 20
    }

    [UnityTest]
    public IEnumerator HealthBoost_OutsideBounds_DoesNotIncreaseHealth()
    {
        Vector2 position = new Vector2(9999, 9999); // Out of bounds
        healthBoostItem.transform.position = position;

        float initialHealth = playerHealth.currentHealth;

        // Simulate picking up the health boost item
        healthBoostScript.OnPickup(playerHealth);
        yield return null;

        Assert.AreEqual(initialHealth, playerHealth.currentHealth); // Health shouldn't change
    }

    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth = 50f;
    }

    public class HealthBoost : MonoBehaviour
    {
        public float boostAmount = 20f;

        public void OnPickup(PlayerHealth playerHealth)
        {
            playerHealth.currentHealth += boostAmount;
        }
    }
}
