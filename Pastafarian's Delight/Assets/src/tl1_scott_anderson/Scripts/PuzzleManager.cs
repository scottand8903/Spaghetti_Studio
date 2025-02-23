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
    //Collin added
    private static Puzzle _instance;
    public static Puzzle Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Puzzle instance has not been initialized! Make sure to call Puzzle.Initialize(dishes) before accessing it.");
            }
            return _instance;
        }
    }

    public PastaDish currentDish;

    public Puzzle(PastaDish[] dishes) : base(dishes)
    {

        currentDish = PickPastaDish();

        if(currentDish != null)
        {
            // PrintCurrentDish();
        }
        else
        {
            Debug.LogError("No dish was selected");
        }
    }

    public static void CreateInstance(PastaDish[] dishes)
    {
        Debug.Log("⚡ Attempting to create Puzzle instance...");
        if(_instance == null)
        {
            _instance = new Puzzle(dishes);
            Debug.Log("Puzzle instance created");
        }
        else
        {
            Debug.LogWarning("⚠ Puzzle instance already exists!");
        }
    }





    public PastaDish GetPastaDish()
    {
        if(currentDish == null)
        {
            return null;
        }
        return currentDish;
    }





    public void PrintCurrentDish()
    {
        if(currentDish == null)
        {
            Debug.LogError("No current dish to display");
            return;
        }
        
        Debug.Log($"Selected Pasta Dish: {currentDish.dishName}");
        foreach(Ingredient ingredient in currentDish.ingredients)
        {
            Debug.Log($"Ingredient: {ingredient.name}  (ID: {ingredient.id})");

            for(int i = 0; i < ingredient.riddles.Length; i++)
            {
                
                Debug.Log($"Riddle {i + 1}: {ingredient.riddles[i]}");
            }
        }
    }
}


[System.Serializable]
public class PastaDish
{
    public string dishName;
    // public string[] ingredients = new string[3];
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
public class PastaDishList
{
    public PastaDish[] dishes;
}


[System.Serializable]
public class PastaDishListWrapper
{
    public PastaDishData[] dishes;
}