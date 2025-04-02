using UnityEngine;

[System.Serializable]
// Represents an individual ingredient with its properties.
public class Ingredient
{
    public int id; // Unique identifier for the ingredient.
    public string name; // Name of the ingredient.
    public string[] riddles; // Array of riddles associated with the ingredient.
    public string sprite; // Path or name of the sprite representing the ingredient.
}

[System.Serializable]
// Represents a collection of ingredients.
public class IngredientList
{
    public Ingredient[] ingredients; // Array of Ingredient objects.
}
