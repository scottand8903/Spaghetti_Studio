using UnityEngine;

[System.Serializable]
public class PastaDish
{
    public string dishName;
    public Ingredient[] ingredients;

    public PastaDish(string name, Ingredient[] ingred)
    {
        this.dishName = name;
        this.ingredients = ingred;
    }
}


[System.Serializable]
public class PastaDishData
{
    public string dishName;
    public string[] ingredients;
}

[System.Serializable]
public class PastaDishListWrapper
{
    public PastaDishData[] dishes;
}