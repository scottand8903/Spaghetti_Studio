using UnityEngine;
using System.IO;

/// <summary>
/// Responsible for loading pasta dishes from a JSON file and initializing them in the puzzle.
/// </summary>
public class PastaDishLoader : MonoBehaviour
{
    private IngredientLoader ingredientLoader; // Reference to the IngredientLoader in the scene
    private Ingredient[] allIngredients; // Array of all available ingredients
    public PastaDish[] loadedDishes; // Array of loaded pasta dishes

    /// <summary>
    /// Called once before the first execution of Update after the MonoBehaviour is created.
    /// Initializes the ingredient loader, loads pasta dishes, and sets up the puzzle.
    /// </summary>
    void Start()
    {
        // Find the IngredientLoader in the scene
        ingredientLoader = FindFirstObjectByType<IngredientLoader>();

        if (ingredientLoader != null)
        {
            // Fetch all ingredients from the IngredientLoader
            allIngredients = ingredientLoader.GetIngredients();
        }
        else
        {
            Debug.LogError("IngredientLoader not found in scene");
            return;
        }

        // Load pasta dishes from the JSON file
        loadedDishes = LoadPastaDishesFromJson();
        if (loadedDishes != null)
        {
            // Initialize the puzzle with the loaded dishes and ingredients
            Puzzle.CreateInstance(loadedDishes, ingredientLoader);
            Debug.Log($"Loaded {loadedDishes.Length} pasta dishes into the puzzle");
        }
        else
        {
            Debug.LogError("No pasta dishes loaded");
        }
    }

    /// <summary>
    /// Loads pasta dishes from a JSON file located in the Resources folder.
    /// </summary>
    /// <returns>An array of PastaDish objects, or null if loading fails.</returns>
    private PastaDish[] LoadPastaDishesFromJson()
    {
        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("pastaDishes");

        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found in Resources folder");
            return null;
        }

        // Parse the JSON file into an array of PastaDishData objects
        PastaDishData[] dishDataList = JsonUtility.FromJson<PastaDishListWrapper>(jsonFile.text).dishes;

        if (dishDataList == null || dishDataList.Length == 0)
        {
            Debug.LogError("Failed to parse pasta dishes");
            return null;
        }

        // Convert the parsed data into an array of PastaDish objects
        PastaDish[] finalDishes = new PastaDish[dishDataList.Length];

        for (int i = 0; i < dishDataList.Length; i++)
        {
            finalDishes[i] = new PastaDish(dishDataList[i].dishName, ConvertIngredientNamesToObjects(dishDataList[i].ingredients));
        }

        return finalDishes;
    }

    /// <summary>
    /// Converts an array of ingredient names into an array of Ingredient objects.
    /// </summary>
    /// <param name="ingredientNames">Array of ingredient names to convert.</param>
    /// <returns>An array of Ingredient objects.</returns>
    private Ingredient[] ConvertIngredientNamesToObjects(string[] ingredientNames)
    {
        Ingredient[] ingredientObjects = new Ingredient[ingredientNames.Length];

        for (int i = 0; i < ingredientNames.Length; i++)
        {
            // Find the Ingredient object by its name
            ingredientObjects[i] = FindIngredientByName(ingredientNames[i]);

            if (ingredientObjects[i] == null)
            {
                Debug.LogError($"Ingredient '{ingredientNames[i]}' not found in ingredients");
            }
        }
        return ingredientObjects;
    }

    /// <summary>
    /// Finds an Ingredient object by its name from the list of all ingredients.
    /// </summary>
    /// <param name="name">The name of the ingredient to find.</param>
    /// <returns>The Ingredient object if found, or null if not found.</returns>
    private Ingredient FindIngredientByName(string name)
    {
        if (allIngredients == null || allIngredients.Length == 0)
        {
            Debug.LogError("Ingredient list is empty or not initialized");
            return null;
        }

        // Iterate through all ingredients to find a match by name
        foreach (var ingredient in allIngredients)
        {
            if (ingredient.name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
            {
                return ingredient;
            }
        }

        Debug.LogWarning($"Ingredient '{name}' not found");
        return null; // Return null if the ingredient is not found
    }
}
