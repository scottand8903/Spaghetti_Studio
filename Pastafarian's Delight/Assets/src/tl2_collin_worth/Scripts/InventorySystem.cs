using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Public Variables
    public Image[] inventorySlotsImages;
    public Sprite emptyImage;
    public Sprite[] itemSprites;
    public int[] inventory;

    // Private Variables

    // This is the maximum number of slots in the inventory
    private int MAX_SPACE = 3;

    // Initialize the singleton instance
    public static InventorySystem Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps inventory across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        initInventory();
    }


    void initInventory()
    {
        // Check if inventory data exists in PlayerPrefs
        if (PlayerPrefs.HasKey("inventoryData"))
        {
            LoadInventory();
        }
        else
        {
            inventory = new int[MAX_SPACE];
            for (int i = 0; i < MAX_SPACE; i++)
            {
                inventory[i] = 0; // Empty slot
            }
        }

        UpdateUI();
    }

    // Adds an item to the inventory
    // Returns 0 if inventory is full, 1 if item is added
    public int addItem(int id, Sprite itemSprite)
    {
        Debug.Log("Trying to add item");
        if (id < 50) // Item is an ingredient
        {
            int emptySlot = nextEmpty();
            if (emptySlot < 0)
            {
                Debug.Log("No Free Slots In Inventory");
                return 0; // Inventory is full
            }

            inventory[emptySlot] = id;
            inventorySlotsImages[emptySlot].sprite = itemSprite;

            Debug.Log("Item Added");
            return 1; // Successfully added
        }
        return 0;
    }

    // Returns the ID of the item in the inventory at the given index
    public int[] GetInventoryIDs()
    {
        // Since it is a clone you will not beable to modify inventory with this function
        return (int[])inventory.Clone();
    }

    // Returns the Id of the inventory slot that is empty 
    // Returns -1 if no empty slots are found
    int nextEmpty()
    {
        for (int i = 0; i < MAX_SPACE; i++)
        {
            if (inventory[i] == 0)
            {
                return i; // Found an empty slot
            }
        }
        return -1; // No empty slots
    }

    // Removes all items from the inventory
    public void wipeInventory(){
        for(int i = 0; i < MAX_SPACE; i ++){
            inventory[i] = 0;
            inventorySlotsImages[i].sprite = emptyImage; // Make this whatevery you want has to be somthing tho
        }
    }

    void SaveInventory()
    {
        // Save the inventory to PlayerPrefs
        for (int i = 0; i < MAX_SPACE; i++)
        {
            PlayerPrefs.SetInt("inventorySlot" + i, inventory[i]);
        }
        PlayerPrefs.Save();
    }

    void LoadInventory()
    {
        // Load the inventory from PlayerPrefs
        inventory = new int[MAX_SPACE];
        for (int i = 0; i < MAX_SPACE; i++)
        {
            inventory[i] = PlayerPrefs.GetInt("inventorySlot" + i, 0);
        }
    }

    // Used when initializing Inventory
    void UpdateUI()
    {
        for (int i = 0; i < MAX_SPACE; i++)
        {
            if (inventory[i] != 0)
            {
                inventorySlotsImages[i].sprite = itemSprites[inventory[i]];
            }
            else
            {
                inventorySlotsImages[i].sprite = emptyImage;
            }
        }
    }
}