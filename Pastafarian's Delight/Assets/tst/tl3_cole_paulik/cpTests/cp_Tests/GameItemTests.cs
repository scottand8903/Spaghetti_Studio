using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameItemTests
{
    private GameObject item;
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<BoxCollider2D>().isTrigger = true;

        item = new GameObject("Item");
        item.AddComponent<BoxCollider2D>().isTrigger = true;

        var rigidbody = item.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.isKinematic = true;

        var gameItem = item.AddComponent<GameItem>();
        var mockPowerUp = item.AddComponent<MockPowerUp>();
        gameItem.powerUpEffect = mockPowerUp;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(item);
    }

    [UnityTest]
    public IEnumerator GameItem_CollidesWithPlayer_TriggersEffect()
    {
        item.transform.position = player.transform.position;

        yield return new WaitForFixedUpdate(); // Let physics process collision

        var mock = item.GetComponent<MockPowerUp>();
        Assert.IsTrue(mock.effectApplied);
    }

    private class MockPowerUp : MonoBehaviour, IPowerUp
    {
        public bool effectApplied = false;
        public void ApplyEffect()
        {
            effectApplied = true;
        }
    }

    public interface IPowerUp
    {
        void ApplyEffect();
    }

    public class GameItem : MonoBehaviour
    {
        public IPowerUp powerUpEffect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                powerUpEffect?.ApplyEffect();
                Destroy(gameObject); // optional
            }
        }
    }
}
