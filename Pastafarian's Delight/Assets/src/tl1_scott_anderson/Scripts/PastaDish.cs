using UnityEngine;

[System.Serializable]
// Represents a pasta dish with a name and a list of ingredients.
public class PastaDish
{
    public string dishName; // The name of the pasta dish.
    public Ingredient[] ingredients; // Array of ingredients used in the dish.

    // Constructor to initialize a PastaDish with a name and ingredients.
    public PastaDish(string name, Ingredient[] ingred)
    {
        this.dishName = name;
        this.ingredients = ingred;
    }
}

[System.Serializable]
// Represents a simplified version of a pasta dish for data serialization.
public class PastaDishData
{
    public string dishName; // The name of the pasta dish.
    public string[] ingredients; // Array of ingredient names as strings.
}

[System.Serializable]
// Wrapper class to hold a collection of PastaDishData objects.
public class PastaDishListWrapper
{
    public PastaDishData[] dishes; // Array of serialized pasta dishes.
}