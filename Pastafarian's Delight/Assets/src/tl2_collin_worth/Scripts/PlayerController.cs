using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody2D rb = null; 
    private Vector3 moveDirection;

    private int MAX_HEALTH = 5;
    private int currentHealth;

    [SerializeField] private float moveSpeed = 10.0f;


    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerMovement();

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

    public int GetHealth(){
        return currentHealth;
    }

    public void updateHealth(int healthChange){
        if((currentHealth + healthChange) <= MAX_HEALTH){
            currentHealth += healthChange;
        }else{
            currentHealth = MAX_HEALTH;
        }
        GameController.Instance.updateHealthSprites();

        if(currentHealth < 1){
            GameController.Instance.QuitGame(); // change to endGame later
        }
    }
//////
}