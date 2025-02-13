using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Private Variables
    private Rigidbody2D _rb = null; 

    [SerializeField] private float _moveSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       GetPlayerMovement();

    }

    void GetPlayerMovement(){
        bool pressingUp = Input.GetKey(KeyCode.W);
        bool pressingLeft = Input.GetKey(KeyCode.A);
        bool pressingDown = Input.GetKey(KeyCode.S);
        bool pressingRight = Input.GetKey(KeyCode.D);
        
        if(pressingUp){
            transform.Translate(Vector2.up * Time.deltaTime * _moveSpeed); 
        }
        if(pressingDown){
            transform.Translate(Vector2.down * Time.deltaTime * _moveSpeed); 
        }
        if(pressingRight){
            transform.Translate(Vector2.right* Time.deltaTime * _moveSpeed); 
        }
        if(pressingLeft){
            transform.Translate(Vector2.left * Time.deltaTime * _moveSpeed); 
        }
        return;
    }
}
