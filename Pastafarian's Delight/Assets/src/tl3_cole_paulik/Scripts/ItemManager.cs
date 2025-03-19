using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemUse> items = new List<ItemUse>();

    private void Start()
    {
        // Example of manually adding items
        items.Add(new ItemUse { id = 51, itemName = "Sample Item", description = "It's a sample item... It doesn't do much", isAvailable = 1 });

        foreach (var item in items)
        {
            Debug.Log("Item: " + item.itemName + " | Availability: " + item.isAvailable);
        }
    }
}