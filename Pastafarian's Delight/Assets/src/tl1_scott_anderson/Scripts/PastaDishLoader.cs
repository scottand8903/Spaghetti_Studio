using UnityEngine;
using System.IO;

public class PastaDishLoader : MonoBehaviour
{
    // public Puzzle puzzle;
    private IngredientLoader ingredientLoader;
    private Ingredient[] allIngredients;
    public PastaDish[] loadedDishes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ingredientLoader = FindFirstObjectByType<IngredientLoader>();

        if(ingredientLoader != null)
        {
            allIngredients = ingredientLoader.GetIngredients(); // fetch all ingredients
        }
        else
        {
            Debug.LogError("IngredientLoader not found in scene");
            return;
        }

        loadedDishes = LoadPastaDishesFromJson();
        if(loadedDishes != null)
        {
            Puzzle.CreateInstance(loadedDishes);
            // puzzle = new Puzzle(loadedDishes);
            Debug.Log($"Loaded {loadedDishes.Length} pasta dishes into the puzzle");
        }
        else
        {
            Debug.LogError("No pasta dishes loaded");
        }
    }

    private PastaDish[] LoadPastaDishesFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("pastaDishes");


        if(jsonFile == null)
        {
            Debug.LogError("JSON file not found in Resources folder");
            return null;
        }

        PastaDishData[] dishDataList = JsonUtility.FromJson<PastaDishListWrapper>(jsonFile.text).dishes;

        if(dishDataList == null || dishDataList.Length == 0)
        {
            Debug.LogError("Failed to parse pasta dishes");
            return null;
        }

        PastaDish[] finalDishes = new PastaDish[dishDataList.Length];

        for(int i = 0; i < dishDataList.Length; i++)
        {
            finalDishes[i] = new PastaDish(dishDataList[i].dishName, ConvertIngredientNamesToObjects(dishDataList[i].ingredients));
        }

        return finalDishes;
    }


    private Ingredient[] ConvertIngredientNamesToObjects(string[] ingredientNames)
    {
        Ingredient[] ingredientObjects = new Ingredient[ingredientNames.Length];

        for(int i = 0; i < ingredientNames.Length; i++)
        {
            ingredientObjects[i] = FindIngredientByName(ingredientNames[i]);

            if(ingredientObjects[i] == null)
            {
                Debug.LogError($"Ingredient '{ingredientNames[i]}' not found in ingredients");
            }
        }
        return ingredientObjects;
    }


    private Ingredient FindIngredientByName(string name)
    {

        if(allIngredients == null || allIngredients.Length == 0)
        {
            Debug.LogError("Ingredient list is empty or not initialized");
            return null;
        }

        // Debug.Log($"allIngredients count : {allIngredients.Length}");
        // Debug.Log($"searching for ingredient: {name}");

        foreach(var ingredient in allIngredients)
        {
            // Debug.Log($"Checking ingredient: {ingredient.name}");
            if(ingredient.name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
            {
                // Debug.Log($"✅ Found ingredient: {ingredient.name} (ID: {ingredient.id})");
                return ingredient;
            }
        }

        Debug.LogWarning($"Ingredient '{name}' not found");
        return null; // return null if not found
    }
}
