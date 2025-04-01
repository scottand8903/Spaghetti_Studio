using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Image[] inventorySlotsImages;
    public Sprite emptyImage;
    public Sprite[] itemSprites;
    private int[] inventory;
    private int MAX_SPACE = 5;

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

            //SaveInventory();
            //UpdateUI();

            Debug.Log("Item Added");
            return 1; // Successfully added
        }
        return 0;
    }

    int nextEmpty()
    {
        for (int i = 0; i < MAX_SPACE; i++)
        {
            if (inventory[i] == 0)
            {
                return i;
            }
        }
        return -1;
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