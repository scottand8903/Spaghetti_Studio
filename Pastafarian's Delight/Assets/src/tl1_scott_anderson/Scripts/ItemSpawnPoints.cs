using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using System.Linq;

public class ItemSpawnPoints : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ingredientPrefab;

    // public Sprite testSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnIngredients();
    }

    void SetIngredientToFront(GameObject ingredient)
    {
        SpriteRenderer spriteRenderer = ingredient.GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            // Set Sorting Layer to "Foreground"
            spriteRenderer.sortingLayerName = "Foreground"; 
            spriteRenderer.sortingOrder = 5;  // Higher order for front layer
        }
    }

    void SpawnIngredients()
    {
        if(Puzzle.Instance == null || Puzzle.Instance.currentDish == null)
        {
            Debug.LogWarning("no active puzzle or dish found");
            return;
        }

        List<Ingredient> availIngredients = Puzzle.Instance.GetAllIngredients();

        if(availIngredients == null || availIngredients.Count == 0)
        {
            Debug.LogError("ingredient list is empty");
            return;
        }

        // Get one correct ingredient from the current dish
        Ingredient correctIngredient = Puzzle.Instance.GetPastaDish().ingredients[Random.Range(0, Puzzle.Instance.GetPastaDish().ingredients.Length)];

        List<Ingredient> incorrectIngredients = availIngredients
            .Where(ing => !Puzzle.Instance.GetPastaDish().ingredients.Contains(ing)) // Exclude correct ingredients
            .OrderBy(x => Random.value) // Shuffle
            .Take(4) // Select four
            .ToList();

        // Combine and shuffle
        List<Ingredient> selectedIngredients = new List<Ingredient> { correctIngredient };
        selectedIngredients.AddRange(incorrectIngredients);
        selectedIngredients = selectedIngredients.OrderBy(x => Random.value).ToList(); // Shuffle


        for(int i = 0; i < selectedIngredients.Count && i < spawnPoints.Length; i++)
        {
            GameObject ingredientObj = Instantiate(ingredientPrefab, spawnPoints[i].position, Quaternion.identity);
            SetIngredientToFront(ingredientObj);
            ingredientObj.name = selectedIngredients[i].name;

            IngredientDisplay display = ingredientObj.GetComponent<IngredientDisplay>();
            if(display != null)
            {
                display.SetIngredient(selectedIngredients[i]);
            }


            // SpriteRenderer spriteRenderer = ingredientObj.GetComponent<SpriteRenderer>();
            // if(spriteRenderer != null)
            // {
            //     spriteRenderer.sprite = testSprite;
            // }
        }
    }
}
