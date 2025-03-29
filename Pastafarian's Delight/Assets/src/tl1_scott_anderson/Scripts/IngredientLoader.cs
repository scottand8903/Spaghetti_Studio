using UnityEngine;
using System.IO;

public class IngredientLoader : MonoBehaviour
{

    public Ingredient[] ingredients;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ingredients = LoadIngredientsFromJson();

        if(ingredients == null)
        {
            Debug.LogError("No ingredients loaded.");
        }
    }


    private Ingredient[] LoadIngredientsFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("ingredient_data");

        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found in Resources folder.");
            return null;
        }

        IngredientList ingredientList = JsonUtility.FromJson<IngredientList>(jsonFile.text);

        if(ingredientList != null && ingredientList.ingredients.Length > 0)
        {
            return ingredientList.ingredients;
        }
        else
        {
            Debug.LogError("Failed to parse ingredients");
            return null;
        } 
    }

    public Ingredient[] GetIngredients()
    {
        return ingredients;
    }
}
