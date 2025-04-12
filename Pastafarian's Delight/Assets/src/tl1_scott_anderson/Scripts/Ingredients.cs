using UnityEngine;

[System.Serializable]
// Represents an individual ingredient with its properties.
public class Ingredient
{
    public int id; // Unique identifier for the ingredient.
    public string name; // Name of the ingredient.
    public string[] riddles; // Array of riddles associated with the ingredient.
    public string sprite; // Path or name of the sprite representing the ingredient.

    public virtual void OnPickup()
    {
        Debug.Log($"{name} Picked up generic ingredient.");
    }

    // public void OnPickup()
    // {
    //     Debug.Log($"{name} Picked up generic ingredient.");
    // }
}

[System.Serializable]
// Represents a collection of ingredients.
public class IngredientList
{
    public Ingredient[] ingredients; // Array of Ingredient objects.
}


public class SpicyIngredient : Ingredient
{
    public override void OnPickup()
    {
        Debug.Log($"{name} Spicy! This ingredient might make the dish hot!.");
    }
    
    // public void OnPickup()
    // {
    //     Debug.Log($"{name} Spicy! This ingredient might make the dish hot!.");
    // }
}
public class SweetIngredient : Ingredient
{
    public override void OnPickup()
    {
        Debug.Log($"{name} Sweet! Adds a sugary touch.");
    }

    // public void OnPickup()
    // {
    //     Debug.Log($"{name} Sweet! Adds a sugary touch.");
    // }
}
