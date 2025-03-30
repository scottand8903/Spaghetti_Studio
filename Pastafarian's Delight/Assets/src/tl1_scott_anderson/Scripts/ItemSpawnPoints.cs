using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawnPoints : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ingredientPrefab;
    public string roomName;
    private Dictionary<int, Ingredient> currentRoomIngredients;


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
        if(currentRoomIngredients == null || currentRoomIngredients.Count == 0)
        {
            // no saved ingredients, generate new ones
            List<Ingredient> newIngredients = GenerateIngredients();
            currentRoomIngredients = new Dictionary<int, Ingredient>();

            for(int i = 0; i < newIngredients.Count; i++)
            {
                currentRoomIngredients[i] = newIngredients[i];
            }

            GameStateManager.Instance.SaveRoomIngredients(roomName, currentRoomIngredients);
        }

        // Log stored ingredients
        Debug.Log($"Room {roomName} - Loaded {currentRoomIngredients.Count} ingredients from saved data.");

        // Filter out collected ingredients
        Dictionary<int, Ingredient> remainingIngredients = currentRoomIngredients
            .Where(kvp => !GameStateManager.Instance.IsIngredientCollected(kvp.Value.id))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Log remaining ingredients
        Debug.Log($"Room {roomName} - Remaining Ingredients after filtering collected ones: {remainingIngredients.Count}");

        SpawnIngredients(remainingIngredients);
    }

    List<Ingredient> GenerateIngredients()
    {
        List<Ingredient> availIngredients = Puzzle.Instance.GetAllIngredients();

        if(availIngredients == null || availIngredients.Count == 0)
        {
            Debug.LogError("ingredient list is empty");
            return new List<Ingredient>();
        }

        // Filter ingredients based on room
        List<Ingredient> filteredIngredients = new List<Ingredient>();

        if(roomName == "EastRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 0 && ing.id <= 10).ToList();
        }
        else if(roomName == "SouthRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 11 && ing.id <= 28).ToList();
        }
        else if(roomName == "WestRoom")
        {
            filteredIngredients = availIngredients.Where(ing => ing.id >= 29 && ing.id <= 44).ToList();
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

        // Shuffle order of selected ingredients and return
        return selectedIngredients.OrderBy(x => Random.value).ToList();

    }

    void SpawnIngredients(Dictionary<int, Ingredient> ingredientData)
    {
        Debug.Log($"Spawning {ingredientData.Count} ingredients in {roomName}");

        foreach(var kvp in ingredientData)
        {
            int spawnPointIndex = kvp.Key;
            Ingredient ingredient = kvp.Value;

            if (spawnPointIndex < 0 || spawnPointIndex >= spawnPoints.Length)
            {
                Debug.LogWarning($"Invalid spawn point index {spawnPointIndex}, skipping.");
                continue;
            }

            Transform spawnPoint = spawnPoints[spawnPointIndex];
            Debug.Log($"Spawning {ingredient.name} at SpawnPoint[{spawnPointIndex}] {spawnPoint.position}");            

            GameObject ingredientObj = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
            SetIngredientToFront(ingredientObj);
            ingredientObj.name = ingredient.name;

            IngredientDisplay display = ingredientObj.GetComponent<IngredientDisplay>();
            if(display != null)
            {
                display.SetIngredient(ingredient);
            }

            Item pickup = ingredientObj.GetComponent<Item>();
            if(pickup != null)
            {
                pickup.ingredientID = ingredient.id;
            }
        }

    }

}
