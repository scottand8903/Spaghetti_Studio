using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages the game state, including tracking ingredients in rooms and collected ingredients.
/// This class is implemented as a singleton to ensure a single instance persists across scenes.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    // Singleton instance of the GameStateManager
    public static GameStateManager Instance;

    // Stores ingredients for each room, where the key is the room name and the value is a dictionary of ingredient IDs and their data
    private Dictionary<string, Dictionary<int, Ingredient>> roomIngredients = new Dictionary<string, Dictionary<int, Ingredient>>();

    // Tracks the IDs of ingredients that have been collected
    private HashSet<int> collectedIngredients = new HashSet<int>();

    /// <summary>
    /// Ensures that only one instance of GameStateManager exists and persists across scenes.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents this object from being destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    /// <summary>
    /// Saves the ingredient data for a specific room.
    /// </summary>
    /// <param name="roomName">The name of the room.</param>
    /// <param name="ingredientData">A dictionary of ingredient IDs and their corresponding data.</param>
    public void SaveRoomIngredients(string roomName, Dictionary<int, Ingredient> ingredientData)
    {
        roomIngredients[roomName] = new Dictionary<int, Ingredient>(ingredientData); // Deep copy the data
        // Debug.Log($"Saving {ingredientData.Count} ingredients for {roomName}");
    }

    /// <summary>
    /// Retrieves the ingredient data for a specific room.
    /// If the room does not exist, initializes a new entry for it.
    /// </summary>
    /// <param name="roomName">The name of the room.</param>
    /// <returns>A dictionary of ingredient IDs and their corresponding data.</returns>
    public Dictionary<int, Ingredient> GetRoomIngredients(string roomName)
    {
        if (!roomIngredients.ContainsKey(roomName))
        {
            Debug.LogWarning($"Room '{roomName}' not found in saved positions. Initializing new entry.");
            roomIngredients[roomName] = new Dictionary<int, Ingredient>(); // Initialize new room entry
        }

        return new Dictionary<int, Ingredient>(roomIngredients[roomName]); // Return a copy of the data
    }

    /// <summary>
    /// Marks an ingredient as collected by adding its ID to the collectedIngredients set.
    /// </summary>
    /// <param name="ingredientID">The ID of the ingredient to collect.</param>
    public void CollectIngredient(int ingredientID)
    {
        collectedIngredients.Add(ingredientID);
    }

    /// <summary>
    /// Checks if an ingredient has already been collected.
    /// </summary>
    /// <param name="ingredientID">The ID of the ingredient to check.</param>
    /// <returns>True if the ingredient has been collected, otherwise false.</returns>
    public bool IsIngredientCollected(int ingredientID)
    {
        return collectedIngredients.Contains(ingredientID);
    }

    public void ResetCollectedIngredients()
    {
        collectedIngredients.Clear();
        // Debug.Log("Collected ingredients reset.");
    }

    public void ResetAllRooms()
    {
        foreach(var roomName in roomIngredients.Keys.ToList())
        {
            roomIngredients[roomName].Clear();
            // Debug.Log($"All ingredients in room {roomName} have been reset.");
        }
    }
}
