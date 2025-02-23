using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Private Variables
    private Rigidbody2D rb = null; 
    

    //Test script
    private InventorySystem inventorySystem;
    public Sprite circleSprite;
    //////

    [SerializeField] private float moveSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        //Test inventory

    }

    // Update is called once per frame
    void Update()
    {
       GetPlayerMovement();


        //Test for adding to inventory
        if(Input.GetKey(KeyCode.I)){
            Debug.Log("sending request to add item");
            inventorySystem.addItem(1, circleSprite);
        }
    }

    void GetPlayerMovement(){
        bool pressingUp = Input.GetKey(KeyCode.W);
        bool pressingLeft = Input.GetKey(KeyCode.A);
        bool pressingDown = Input.GetKey(KeyCode.S);
        bool pressingRight = Input.GetKey(KeyCode.D);
        
        if(pressingUp){
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed); 
        }
        if(pressingDown){
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed); 
        }
        if(pressingRight){
            transform.Translate(Vector2.right* Time.deltaTime * moveSpeed); 
        }
        if(pressingLeft){
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed); 
        }
        return;
    }
}
