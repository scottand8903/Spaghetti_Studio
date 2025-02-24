using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    
    public Image[] inventorySlotsImages;
    public Sprite emptyImage;
    public Sprite[] itemSprites; 
    private int[] inventory;
    private int MAX_SPACE = 5;

    public static InventorySystem Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initInventory();
        CreateInstance();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initInventory(){
        inventory = new int[MAX_SPACE];
        for(int i = 0; i < MAX_SPACE; i ++){
            inventory[i] = 0;
        }
    }

    // Adds item to inventory if there is no availble space it returns 0(not successful) else returns 1(successful)
    // Id is used to know whats there
    // itemSprite is the image that will be shown
    public int addItem(int id, Sprite itemSprite){
        Debug.Log("Trying to add item");
        if(id < 50){ // Item is an ingredient
            // Check slot
            int emptySlot = nextEmpty();
            if(emptySlot < 0){
                return 0; 
            }
            // Add ingredient
            inventory[emptySlot] = id; 
            inventorySlotsImages[emptySlot].sprite = itemSprite;
            return 1;
        }
        return 0;
    }

    public static void CreateInstance()
    {
        Debug.Log("Attempting to create invnetory");
        if(Instance == null)
        {
            Instance = new InventorySystem();
            Debug.Log("Inventory instance created");
        }
        else
        {
            Debug.LogWarning("inventory instance already exists!");
        }
    }

    int nextEmpty(){
        for(int i = 0; i < MAX_SPACE; i ++){
            if(inventory[i] == 0){
                return i;
            }
        }
        return -1;
    } 
}
