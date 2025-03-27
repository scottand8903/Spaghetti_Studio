using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public int isAvailable;
    [SerializeField] public Sprite itemSprite;

    private void Start()
    {
        Debug.Log("Item " + itemName + " availability: " + isAvailable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + itemName);

            PowerUp powerUp = GetComponent<PowerUp>();
            if (powerUp != null)
            {
                powerUp.ApplyEffect(collision.gameObject);
            }

            Destroy(gameObject); // Remove item after collection
        }
    }
}
