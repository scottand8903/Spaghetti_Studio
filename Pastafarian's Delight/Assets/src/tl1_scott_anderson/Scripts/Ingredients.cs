using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public int id;
    public string name;
    public string[] riddles;
    public Sprite sprite;
}


[System.Serializable]
public class IngredientList
{
    public Ingredient[] ingredients;
}
