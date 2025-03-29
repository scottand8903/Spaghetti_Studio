using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puzzle : PuzzleManager
{
    private static Puzzle _instance;
    public PastaDish currentDish;
    private IngredientLoader ingredientLoader;
    public static Puzzle Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Puzzle instance has not been initialized! Make sure to call Puzzle.Initialize(dishes) before accessing it.");
            }
            return _instance;
        }
    }

    public Puzzle(PastaDish[] dishes, IngredientLoader loader) : base(dishes)
    {
        ingredientLoader = loader;
        if(ingredientLoader == null)
        {
            Debug.LogError("Ingredientload is null");
        }

        currentDish = PickPastaDish();

        if(currentDish != null)
        {
            // PrintCurrentDish();
        }
        else
        {
            Debug.LogError("No dish was selected");
        }
    }

    public static void CreateInstance(PastaDish[] dishes, IngredientLoader loader)
    {
        Debug.Log("Attempting to create Puzzle instance...");
        if(_instance == null)
        {
            _instance = new Puzzle(dishes, loader);
            Debug.Log("Puzzle instance created");
        }
        else
        {
            Debug.LogWarning("Puzzle instance already exists!");
        }
    }

    public PastaDish GetPastaDish()
    {
        if(currentDish == null)
        {
            return null;
        }
        return currentDish;
    }

    public List<Ingredient> GetAllIngredients()
    {
        if(ingredientLoader == null)
        {
            Debug.LogError("ingredientloader is not set in puzzle");
            return new List<Ingredient>();
        }

        return ingredientLoader.GetIngredients().ToList();
    }

    public void PrintCurrentDish()
    {
        if(currentDish == null)
        {
            Debug.LogError("No current dish to display");
            return;
        }
        
        Debug.Log($"Selected Pasta Dish: {currentDish.dishName}");
        foreach(Ingredient ingredient in currentDish.ingredients)
        {
            Debug.Log($"Ingredient: {ingredient.name}  (ID: {ingredient.id})");

            for(int i = 0; i < ingredient.riddles.Length; i++)
            {
                
                Debug.Log($"Riddle {i + 1}: {ingredient.riddles[i]}");
            }
        }
    }
}