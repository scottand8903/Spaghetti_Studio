using NUnit.Framework.Internal;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public bool isAvailable;
    [SerializeField] public Sprite itemSprite;


    private void Start()
    {
        if (isAvailable)
        {
            Debug.Log("Item " + itemName + " is available.");
        }
        else
        {
            Debug.Log("Item " + itemName + " is not available.");
        }
    }
}
