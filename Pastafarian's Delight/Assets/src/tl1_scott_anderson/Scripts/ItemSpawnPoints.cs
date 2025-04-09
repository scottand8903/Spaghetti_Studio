using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// Handles the spawning of ingredients in specific rooms based on the current puzzle state.
/// </summary>
public class ItemSpawnPoints : MonoBehaviour
{
    // Array of spawn points where ingredients can appear
    public Transform[] spawnPoints;

    // Prefab for the ingredient GameObject
    public GameObject ingredientPrefab;

    // Name of the room this script is associated with
    public string roomName;

    // Dictionary to store the ingredients currently associated with this room
    private Dictionary<int, Ingredient> currentRoomIngredients;

    /// <summary>
    /// Called once before the first frame update. Initializes the ingredient spawning process.
    /// </summary>
    void Start()
    {
        LoadOrSpawnIngredients();
    }

    /// <summary>
    /// Sets the ingredient GameObject to appear in the foreground by adjusting its sorting layer and order.
    /// </summary>
    /// <param name="ingredient">The ingredient GameObject to modify.</param>
    void SetIngredientToFront(GameObject ingredient)
    {
        SpriteRenderer spriteRenderer = ingredient.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Set Sorting Layer to "Foreground"
            spriteRenderer.sortingLayerName = "Foreground"; 
            spriteRenderer.sortingOrder = 5;  // Higher order for front layer
        }
    }

    /// <summary>
    /// Loads existing ingredients for the room from saved data or generates new ones if none exist.
    /// </summary>
    void LoadOrSpawnIngredients()
    {
        // Ensure there is an active puzzle and dish
        if (Puzzle.Instance == null || Puzzle.Instance.currentDish == null)
        {
            Debug.LogWarning("No active puzzle or dish found");
            return;
        }

        // Attempt to load saved ingredients for the room
        currentRoomIngredients = GameStateManager.Instance.GetRoomIngredients(roomName);
        if (currentRoomIngredients == null || currentRoomIngredients.Count == 0)
        {
            // Generate new ingredients if none are saved
            List<Ingredient> newIngredients = GenerateIngredients();
            currentRoomIngredients = new Dictionary<int, Ingredient>();

            for (int i = 0; i < newIngredients.Count; i++)
            {
                currentRoomIngredients[i] = newIngredients[i];
            }

            // Save the newly generated ingredients
            GameStateManager.Instance.SaveRoomIngredients(roomName, currentRoomIngredients);
        }

        // Log the loaded ingredients
        Debug.Log($"Room {roomName} - Loaded {currentRoomIngredients.Count} ingredients from saved data.");

        // Filter out ingredients that have already been collected
        Dictionary<int, Ingredient> remainingIngredients = currentRoomIngredients
            .Where(kvp => !GameStateManager.Instance.IsIngredientCollected(kvp.Value.id))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Log the remaining ingredients
        Debug.Log($"Room {roomName} - Remaining Ingredients after filtering collected ones: {remainingIngredients.Count}");

        // Spawn the remaining ingredients
        SpawnIngredients(remainingIngredients);
    }

    /// <summary>
    /// Generates a list of ingredients for the room based on the current puzzle and room name.
    /// </summary>
    /// <returns>A list of selected ingredients.</returns>
    List<Ingredient> GenerateIngredients()
    {
        // Get all available ingredients from the puzzle
        List<Ingredient> availIngredients = Puzzle.Instance.GetAllIngredients();

        if (availIngredients == null || availIngredients.Count == 0)
        {
            Debug.LogError("Ingredient list is empty");
            return new List<Ingredient>();
        }

        // Filter ingredients based on the room name
        List<Ingredient> filteredIngredients = new List<Ingredient>();
        if (roomName == "EastRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 0 && ing.id <= 10).ToList();
            Debug.Log($"Filtered {filteredIngredients.Count} ingredients for EastRoom");
        }
        else if (roomName == "SouthRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 11 && ing.id <= 28).ToList();
            Debug.Log($"Filtered {filteredIngredients.Count} ingredients for SouthRoom");
        }
        else if (roomName == "WestRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 29 && ing.id <= 44).ToList();
            Debug.Log($"Filtered {filteredIngredients.Count} ingredients for WestRoom");
        }
        else
        {
            Debug.LogWarning($"Unknown room: {roomName}. Using all ingredients.");
            filteredIngredients = availIngredients;
        }

        Debug.Log($"Room {roomName} - Total filtered ingredients: {filteredIngredients.Count}");

        if (filteredIngredients.Count == 0)
        {
            Debug.LogError($"No valid ingredients found for {roomName}");
            return new List<Ingredient>();
        }

        // Select 5 random ingredients (1 correct, 4 incorrect)
        List<Ingredient> selectedIngredients = new List<Ingredient>();

        // Pick one correct ingredient from the current dish
        Ingredient correctIngredient = Puzzle.Instance.GetPastaDish().ingredients
            .Where(ing => filteredIngredients.Contains(ing)) // Ensure it's in the filtered list
            .OrderBy(x => Random.value)
            .FirstOrDefault();

        if (correctIngredient != null)
        {
            selectedIngredients.Add(correctIngredient);
            filteredIngredients.Remove(correctIngredient);
        }

        // Pick 4 incorrect ingredients from the filtered list
        selectedIngredients.AddRange(filteredIngredients.OrderBy(x => Random.value).Take(4));

        // Shuffle the selected ingredients and return
        return selectedIngredients.OrderBy(x => Random.value).ToList();
    }

    /// <summary>
    /// Spawns the given ingredients at their respective spawn points in the room.
    /// </summary>
    /// <param name="ingredientData">A dictionary of spawn point indices and their corresponding ingredients.</param>
    void SpawnIngredients(Dictionary<int, Ingredient> ingredientData)
    {
        Debug.Log($"Spawning {ingredientData.Count} ingredients in {roomName}");

        foreach (var kvp in ingredientData)
        {
            int spawnPointIndex = kvp.Key;
            Ingredient ingredient = kvp.Value;

            // Validate the spawn point index
            if (spawnPointIndex < 0 || spawnPointIndex >= spawnPoints.Length)
            {
                Debug.LogWarning($"Invalid spawn point index {spawnPointIndex}, skipping.");
                continue;
            }

            Transform spawnPoint = spawnPoints[spawnPointIndex];
            Debug.Log($"Spawning {ingredient.name} at SpawnPoint[{spawnPointIndex}] {spawnPoint.position}");

            // Instantiate the ingredient GameObject using the ingredient prefab
            GameObject ingredientObj = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
            ingredientObj.name = ingredient.name;
            Debug.Log($"Instantiated ingredient object: {ingredientObj.name}");

            // Verify the hierarchy of the instantiated object
            Transform labelTransform = ingredientObj.transform.Find("IngredientLabel/Canvas/IngredientName");
            if (labelTransform != null)
            {
                Debug.Log($"Found IngredientName under {ingredientObj.name}");
            }
            else
            {
                Debug.LogError($"IngredientLabel/Canvas/IngredientName not found under {ingredientObj.name}");
            }



            SetIngredientToFront(ingredientObj);
            

            // Set the ingredient data on the display component
            IngredientDisplay display = ingredientObj.GetComponent<IngredientDisplay>();
            if (display != null)
            {
                display.SetIngredient(ingredient, ingredientObj);
            }

            // Set the ingredient ID on the pickup component
            Item pickup = ingredientObj.GetComponent<Item>();
            if (pickup != null)
            {
                pickup.ingredientID = ingredient.id;
            }
        }
    }
}
