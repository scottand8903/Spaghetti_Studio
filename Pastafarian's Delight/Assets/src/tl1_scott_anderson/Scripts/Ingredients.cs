using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public int id;
    public string name;
    public string[] riddles;
}


[System.Serializable]
public class IngredientList
{
    public Ingredient[] ingredients;
}
