using UnityEngine;
using System.IO;

public class PastaDishLoader : MonoBehaviour
{
    private Puzzle puzzle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PastaDish[] loadedDishes = LoadPastaDishesFromJson();
        // Debug.Log(loadedDishes.Length);
        if(loadedDishes != null)
        {
            puzzle = new Puzzle(loadedDishes);
        }
    }

    private PastaDish[] LoadPastaDishesFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("pastaDishes");

        if(jsonFile != null)
        {
            PastaDishList dishList = JsonUtility.FromJson<PastaDishList>(jsonFile.text);

            if(dishList != null && dishList.dishes.Length > 0)
            {
                return dishList.dishes;
            }
            else
            {
                Debug.LogError("Failed to parse pasta dishes");
            }
        }
        else
        {
            Debug.LogError("JSON file not found");
        }
        return null;
    }

}
