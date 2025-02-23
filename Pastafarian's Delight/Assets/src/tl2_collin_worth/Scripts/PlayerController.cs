using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody2D rb = null; 
    private Vector3 moveDirection;

    // Test script
    private InventorySystem inventorySystem;
    public Sprite circleSprite;
    
    [SerializeField] private float moveSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerMovement();

        // Test for adding to inventory
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("sending request to add item");
            inventorySystem.addItem(1, circleSprite);
        }
    }

    void GetPlayerMovement()
    {
        bool pressingUp = Input.GetKey(KeyCode.W);
        bool pressingLeft = Input.GetKey(KeyCode.A);
        bool pressingDown = Input.GetKey(KeyCode.S);
        bool pressingRight = Input.GetKey(KeyCode.D);

        // Reset velocity each frame to handle movement correctly
        rb.linearVelocity = Vector2.zero;

        // Handle movement based on player input
        if (pressingUp)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed); // Move up
        }
        if (pressingDown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed); // Move down
        }
        if (pressingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y); // Move right
        }
        if (pressingLeft)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y); // Move left
        }
    }

    // Stop the player's movement on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop movement by zeroing the Rigidbody velocity
            rb.linearVelocity = Vector2.zero;
        }
    }
//////
}