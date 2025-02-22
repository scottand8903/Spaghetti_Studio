using UnityEngine;
using System.IO;

public class IngredientLoader : MonoBehaviour
{

    private Ingredient[] ingredients;
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


    // private void PrintRandomIngredient()
    // {
    //     if (ingredients == null || ingredients.Length == 0)
    //     {
    //         Debug.LogError("No ingredients available to print.");
    //         return;
    //     }

    //     int randomIndex = Random.Range(0, ingredients.Length);
    //     Ingredient randomIngredient = ingredients[randomIndex];

    //     Debug.Log($"Random Index: {randomIndex}");
    //     Debug.Log($"ID: {randomIngredient.id}");
    //     Debug.Log($"Name: {randomIngredient.name}");

    //     for (int i = 0; i < randomIngredient.riddles.Length; i++)
    //     {
    //         Debug.Log($"Riddle {i + 1}: {randomIngredient.riddles[i]}");
    //     }
    // }

}
