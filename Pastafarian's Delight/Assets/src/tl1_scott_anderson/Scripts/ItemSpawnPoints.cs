using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawnPoints : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ingredientPrefab;
    public string roomName;
    private List<Ingredient> currentRoomIngredients;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadOrSpawnIngredients();
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


    void LoadOrSpawnIngredients()
    {
        if(Puzzle.Instance == null || Puzzle.Instance.currentDish == null)
        {
            Debug.LogWarning("no active puzzle or dish found");
            return;
        }

        // check for existing ingredients for room
        currentRoomIngredients = GameStateManager.Instance.GetRoomIngredients(roomName);

        if(currentRoomIngredients == null)
        {
            // no saved ingredients, generate new ones
            currentRoomIngredients = GenerateIngredients();
            GameStateManager.Instance.SaveRoomIngredients(roomName, currentRoomIngredients);
        }

        SpawnIngredients(currentRoomIngredients);
    }

    List<Ingredient> GenerateIngredients()
    {
        List<Ingredient> availIngredients = Puzzle.Instance.GetAllIngredients();

        if(availIngredients == null || availIngredients.Count == 0)
        {
            Debug.LogError("ingredient list is empty");
            return new List<Ingredient>();
        }

        // // Get one correct ingredient from the current dish
        // Ingredient correctIngredient = Puzzle.Instance.GetPastaDish().ingredients[Random.Range(0, Puzzle.Instance.GetPastaDish().ingredients.Length)];

        // // Get four incorrect ingredients
        // List<Ingredient> incorrectIngredients = availIngredients
        //     .Where(ing => !Puzzle.Instance.GetPastaDish().ingredients.Contains(ing)) // Exclude correct ingredients
        //     .OrderBy(x => Random.value) // Shuffle
        //     .Take(4) // Select four
        //     .ToList();

        // // Combine and shuffle
        // List<Ingredient> selectedIngredients = new List<Ingredient> { correctIngredient };
        // selectedIngredients.AddRange(incorrectIngredients);
        // selectedIngredients = selectedIngredients.OrderBy(x => Random.value).ToList(); // Shuffle

        // Filter ingredients based on room
        List<Ingredient> filteredIngredients = new List<Ingredient>();

        if(roomName == "EastRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 0 && ing.id <= 10).ToList();
        }
        else if(roomName == "SouthRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 11 && ing.id <= 27).ToList();
        }
        else if(roomName == "WestRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 28 && ing.id <= 44).ToList();
        }
        else
        {
            Debug.LogWarning($"Unknown room: {roomName}. Using all ingredients.");
            filteredIngredients = availIngredients;
        }

        if (filteredIngredients.Count == 0)
        {
            Debug.LogError($"No valid ingredients found for {roomName}");
            return new List<Ingredient>();
        }

        // Select 5 random ingredients (1 correct, 4 incorrect)
        List<Ingredient> selectedIngredients = new List<Ingredient>();

        // Pick one correct ingredient from the current dish (if applicable)
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

        return selectedIngredients;

    }

    void SpawnIngredients(List<Ingredient> ingredients)
    {
        for(int i = 0; i < ingredients.Count && i < spawnPoints.Length; i++)
        {
            GameObject ingredientObj = Instantiate(ingredientPrefab, spawnPoints[i].position, Quaternion.identity);
            SetIngredientToFront(ingredientObj);
            ingredientObj.name = ingredients[i].name;

            IngredientDisplay display = ingredientObj.GetComponent<IngredientDisplay>();
            if(display != null)
            {
                display.SetIngredient(ingredients[i]);
            }
        }
    }

}
