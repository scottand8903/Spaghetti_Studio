using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SpeedBoostTests
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private GameObject speedBoostItem;
    private SpeedBoost speedBoostScript;

    [SetUp]
    public void Setup()
    {
        // Setup Player GameObject with PlayerMovement
        player = new GameObject("Player");
        playerMovement = player.AddComponent<PlayerMovement>();
        playerMovement.baseSpeed = 5f; // Default speed
        playerMovement.currentSpeed = 5f;

        // Setup SpeedBoost item
        speedBoostItem = new GameObject("SpeedBoost");
        speedBoostScript = speedBoostItem.AddComponent<SpeedBoost>();
        speedBoostScript.boostAmount = 3f; // Boosts speed by 3

        // Add collider for detection
        speedBoostItem.AddComponent<BoxCollider2D>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(speedBoostItem);
    }

    [UnityTest]
    public IEnumerator SpeedBoost_InsideBounds_IncreasesSpeed()
    {
        Vector2 position = new Vector2(5, 5);
        speedBoostItem.transform.position = position;

        // Simulate picking up the speed boost item
        speedBoostScript.OnPickup(playerMovement);
        yield return null;

        Assert.AreEqual(8f, playerMovement.currentSpeed); // 5 + 3
    }

    [UnityTest]
    public IEnumerator SpeedBoost_OutsideBounds_DoesNotIncreaseSpeed()
    {
        Vector2 position = new Vector2(9999, 9999); // Out of bounds
        speedBoostItem.transform.position = position;

        float initialSpeed = playerMovement.currentSpeed;

        // Simulate picking up the speed boost item
        speedBoostScript.OnPickup(playerMovement);
        yield return null;

        Assert.AreEqual(initialSpeed, playerMovement.currentSpeed); // Speed shouldn't change
    }

    public class PlayerMovement : MonoBehaviour
    {
        public float baseSpeed = 5f;
        public float currentSpeed = 5f;
    }

    public class SpeedBoost : MonoBehaviour
    {
        public float boostAmount = 3f;

        public void OnPickup(PlayerMovement playerMovement)
        {
            playerMovement.currentSpeed += boostAmount;
        }
    }
}
