using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents the Puzzle class, which manages the current pasta dish and its ingredients.
/// </summary>
public class Puzzle : PuzzleManager
{
    /// Singleton instance of the Puzzle class.
    private static Puzzle _instance;

    /// The currently selected pasta dish.
    public PastaDish currentDish;

    /// Loader for retrieving ingredient data.
    private IngredientLoader ingredientLoader;

    /// Gets the singleton instance of the Puzzle class.
    public static Puzzle Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Puzzle instance has not been initialized! Make sure to call Puzzle.Initialize(dishes) before accessing it.");
            }
            return _instance;
        }
    }

    /// <summary>
    /// Creates the singleton instance of the Puzzle class.
    /// </summary>
    /// <param name="dishes">Array of available pasta dishes.</param>
    /// <param name="loader">Ingredient loader for retrieving ingredient data.</param>
    public static void CreateInstance(PastaDish[] dishes, IngredientLoader loader)
    {
        Debug.Log("Attempting to create Puzzle instance...");
        if (_instance == null)
        {
            _instance = new Puzzle(dishes, loader);
            Debug.Log("Puzzle instance created");
        }
        else
        {
            Debug.LogWarning("Puzzle instance already exists!");
        }
    }


    /// <summary>
    /// Initializes a new instance of the Puzzle class.
    /// </summary>
    /// <param name="dishes">Array of available pasta dishes.</param>
    /// <param name="loader">Ingredient loader for retrieving ingredient data.</param>
    public Puzzle(PastaDish[] dishes, IngredientLoader loader) : base(dishes)
    {
        ingredientLoader = loader;
        if (ingredientLoader == null)
        {
            Debug.LogError("IngredientLoader is null");
        }

        currentDish = PickPastaDish();

        if (currentDish == null)
        {
            Debug.LogError("No dish was selected");
        }
    }


    /// <summary>
    /// Gets the currently selected pasta dish.
    /// </summary>
    /// <returns>The current pasta dish, or null if none is selected.</returns>
    public PastaDish GetPastaDish()
    {
        if (currentDish == null)
        {
            return null;
        }
        return currentDish;
    }

    /// <summary>
    /// Retrieves all ingredients from the ingredient loader.
    /// </summary>
    /// <returns>A list of all ingredients, or an empty list if the loader is not set.</returns>
    public List<Ingredient> GetAllIngredients()
    {
        if (ingredientLoader == null)
        {
            Debug.LogError("IngredientLoader is not set in Puzzle");
            return new List<Ingredient>();
        }

        return ingredientLoader.GetIngredients().ToList();
    }

    /// <summary>
    /// Logs the details of the currently selected pasta dish and its ingredients.
    /// </summary>
    public void PrintCurrentDish()
    {
        if (currentDish == null)
        {
            Debug.LogError("No current dish to display");
            return;
        }

        Debug.Log($"Selected Pasta Dish: {currentDish.dishName}");
        foreach (Ingredient ingredient in currentDish.ingredients)
        {
            // Debug.Log($"Ingredient: {ingredient.name}  (ID: {ingredient.id})");

            for (int i = 0; i < ingredient.riddles.Length; i++)
            {
                // Debug.Log($"Riddle {i + 1}: {ingredient.riddles[i]}");
            }
        }
    }
}