using UnityEngine;
using System.Collections;


//////////////////////////////////////////////// Health System (Dynamic Binding) /////////////////////////
///////////////////////////////////////
public class HealthSystem 
{
    public static HealthSystem Instance { get; private set; }

    public int MAX_HEALTH = 5;
    public int currentHealth;

    public HealthSystem()
    {
        if (Instance == null)
        {
            Instance = this; // Assign singleton instance
        }
        else
        {
            Debug.LogWarning("HealthSystemBC instance already exists!"); // Prevent duplicates
        }

        currentHealth = MAX_HEALTH;
    }

    public virtual void updateHealth(int healthChange){
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

    public int GetHealth(){
        return currentHealth;
    }
}

public class HealthSystemBC : HealthSystem
{

    public HealthSystemBC() : base() { }

    public override void updateHealth(int healthChange){
        // Do nothing to health
    }
}
////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody2D rb = null; 
    private Vector3 moveDirection;
    private Animator animator;


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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(WaitForGameRunning());
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void FixedUpdate() 
    {

        GetPlayerMovement();
        
        bool up = Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        // Clear all first
        animator.SetBool("walkingUp", false);
        animator.SetBool("walkingDown", false);
        animator.SetBool("walkingLeft", false);
        animator.SetBool("walkingRight", false);

        // Prioritize directions (up > down > left > right)
        if (up)
            animator.SetBool("walkingUp", true);
        else if (down)
            animator.SetBool("walkingDown", true);
        else if (left)
            animator.SetBool("walkingLeft", true);
        else if (right)
            animator.SetBool("walkingRight", true); 
    }

    private IEnumerator WaitForGameRunning()
    {
        yield return new WaitUntil(() => GameController.Instance.GameRunning);
        
        if(GameController.Instance.BCMode){
            new HealthSystemBC();
        }
        else
        {
            new HealthSystem();
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