using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    
    public Image[] inventorySlotsImages;
    public Sprite emptyImage;
    public Sprite[] itemSprites; 
    private int[] inventory;
    private int MAX_SPACE = 5;

    public static InventorySystem Instance {get; private set; }


    void Awake(){
        if (Instance == null)
        {
            Instance = this;  // ✅ Correct singleton assignment
            DontDestroyOnLoad(gameObject); // Optional: Keeps instance across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents multiple instances
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //if (inventorySlotsImages == null || inventorySlotsImages.Length == 0)
    //{
    //    Debug.LogError("inventorySlotsImages is EMPTY! Check Unity Inspector.");
    //}
    //else
    //{
        Debug.Log($"inventorySlotsImages has {inventorySlotsImages.Length} elements.");
        for (int i = 0; i < inventorySlotsImages.Length; i++)
        {
            Debug.Log($"Slot {i}: {inventorySlotsImages[i]}");
        }
    //}
    initInventory();
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
        if (inventory == null || inventory.Length != MAX_SPACE)
        {
            Debug.LogError("inventory array is not initialized correctly!");
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
            if (emptySlot >= inventorySlotsImages.Length)
            {
                Debug.LogError("emptySlot index is out of bounds!");
                return 0;
            }
            if (inventorySlotsImages[emptySlot] == null)
            {
                Debug.LogError($"inventorySlotsImages[{emptySlot}] is null!");
                return 0;
            }
            inventory[emptySlot] = id; 
            inventorySlotsImages[emptySlot].sprite = itemSprite;
            return 1;
        }
        return 0;
    }

    int nextEmpty(){
        for(int i = 0; i < MAX_SPACE; i ++){
            if(inventory[i] == 0){
                Debug.Log("returning next empty:");
                Debug.Log(i);
                return i;
            }
        }
        return -1;
    } 
}
