using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;
    private Dictionary<string, List<Ingredient>> roomIngredients = new Dictionary<string, List<Ingredient>>();


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveRoomIngredients(string roomName, List<Ingredient> ingredients)
    {
        if(roomIngredients.ContainsKey(roomName))
        {
            roomIngredients[roomName] = ingredients;
        }
        else
        {
            roomIngredients.Add(roomName, ingredients);
        }
    }

    public List<Ingredient> GetRoomIngredients(string roomName)
    {
        if(roomIngredients.ContainsKey(roomName))
        {
            return roomIngredients[roomName];
        }
        else
        {
            return null;
        }
    }

}
