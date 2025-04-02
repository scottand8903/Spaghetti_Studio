using UnityEngine;
using System.IO;

/// <summary>
/// The IngredientLoader class is responsible for loading ingredient data from a JSON file
/// located in the Resources folder and making it available to other parts of the application.
/// </summary>
public class IngredientLoader : MonoBehaviour
{
    // Array to store the loaded ingredients.
    public Ingredient[] ingredients;

    /// <summary>
    /// Called once before the first execution of Update after the MonoBehaviour is created.
    /// Loads the ingredients from the JSON file and logs an error if no ingredients are loaded.
    /// </summary>
    void Start()
    {
        // Load ingredients from the JSON file.
        ingredients = LoadIngredientsFromJson();

        // Log an error if no ingredients are loaded.
        if (ingredients == null)
        {
            Debug.LogError("No ingredients loaded.");
        }
    }

    /// <summary>
    /// Loads ingredient data from a JSON file located in the Resources folder.
    /// </summary>
    /// <returns>An array of Ingredient objects if successful, otherwise null.</returns>
    private Ingredient[] LoadIngredientsFromJson()
    {
        // Load the JSON file from the Resources folder.
        TextAsset jsonFile = Resources.Load<TextAsset>("ingredient_data");

        // Check if the JSON file exists.
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found in Resources folder.");
            return null;
        }

        // Parse the JSON file into an IngredientList object.
        IngredientList ingredientList = JsonUtility.FromJson<IngredientList>(jsonFile.text);

        // Check if the parsing was successful and the list contains ingredients.
        if (ingredientList != null && ingredientList.ingredients.Length > 0)
        {
            return ingredientList.ingredients;
        }
        else
        {
            Debug.LogError("Failed to parse ingredients");
            return null;
        }
    }

    /// <summary>
    /// Provides access to the loaded ingredients.
    /// </summary>
    /// <returns>An array of Ingredient objects.</returns>
    public Ingredient[] GetIngredients()
    {
        return ingredients;
    }
}
