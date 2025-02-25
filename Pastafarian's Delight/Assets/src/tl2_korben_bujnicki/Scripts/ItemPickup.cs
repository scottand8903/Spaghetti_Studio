using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId = 1;
    public Sprite itemSprite;

    private bool isPlayerNearby = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.P))
        {
            TryPickUp();
        }
    }

    void TryPickUp()
    {
        if (InventorySystem.Instance != null)
        {
            int result = InventorySystem.Instance.addItem(itemId, itemSprite);
            if (result == 1)
            {
                Debug.Log("Item picked up and added to inventory!");
                Destroy(gameObject); // Remove item from the scene
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
        else
        {
            Debug.LogError("InventorySystem not found!");
        }
    }


}
