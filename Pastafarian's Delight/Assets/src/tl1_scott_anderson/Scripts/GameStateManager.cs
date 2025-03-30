using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;
    // private Dictionary<string, List<Ingredient>> roomIngredients = new Dictionary<string, List<Ingredient>>();
    // private Dictionary<string, List<string>> collectedIngredients = new Dictionary<string, List<string>>();

    private Dictionary<string, Dictionary<int, Ingredient>> roomIngredients = new Dictionary<string, Dictionary<int, Ingredient>>();
    private HashSet<int> collectedIngredients = new HashSet<int>();


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveRoomIngredients(string roomName, Dictionary<int, Ingredient> ingredientData)
    {
        roomIngredients[roomName] = new Dictionary<int, Ingredient>(ingredientData);
        Debug.Log($"Saving {ingredientData.Count} ingredients for {roomName}");
    }

    public Dictionary<int, Ingredient> GetRoomIngredients(string roomName)
    {
        if (!roomIngredients.ContainsKey(roomName))
        {
            Debug.LogWarning($"Room '{roomName}' not found in saved positions. Initializing new entry.");
            roomIngredients[roomName] = new Dictionary<int, Ingredient>(); // Initialize new room entry
        }

        return new Dictionary<int, Ingredient>(roomIngredients[roomName]);
    }

    public void CollectIngredient(int ingredientID)
    {
        collectedIngredients.Add(ingredientID);
    }
    public bool IsIngredientCollected(int ingredientID)
    {
        return collectedIngredients.Contains(ingredientID);
    }

}
