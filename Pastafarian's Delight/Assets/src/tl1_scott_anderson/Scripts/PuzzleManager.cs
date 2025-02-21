using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    protected List<PastaDish> pastaDishes = new List<PastaDish>();

    public PuzzleManager(PastaDish[] dishes)
    {
        if(dishes != null)
        {
            pastaDishes.AddRange(dishes);
        }
    }

    public virtual PastaDish PickPastaDish()
    {
        if(pastaDishes.Count > 0)
        {
            return pastaDishes[Random.Range(0, pastaDishes.Count)];
        }
        else
        {
            Debug.LogError("No dishes available");
            return null;
        }
    }
}



public class Puzzle : PuzzleManager
{
    private PastaDish currentDish;

    public Puzzle(PastaDish[] dishes) : base(dishes)
    {
        currentDish = PickPastaDish();

        if(currentDish != null)
        {
            Debug.Log("Selected Dish: " + currentDish.dishName);
        }
    }
}



[System.Serializable]
public class PastaDish
{
    public string dishName;
    // public List<string> ingredients {get; set; }
    public string[] ingredients = new string[3];

    public PastaDish(string name, string[] ingred)
    {
        this.dishName = name;
        this.ingredients = ingred;
    }
}


[System.Serializable]
public class PastaDishList
{
    public PastaDish[] dishes;
}