using UnityEngine;
using TMPro;

public class RiddleDisplay : MonoBehaviour
{
    public TMP_Text riddleTextBox;

    public static RiddleDisplay Instance; // Singleton pattern for access from static method

    private void Awake()
    {
        Instance = this;
    }

    public static void DisplayRiddle(string ingredientName)
    {
        PastaDish currentDish = Puzzle.Instance.currentDish;

        int iIndex = 0; 
        foreach (Ingredient ingredient in currentDish.ingredients)
        {
            if (ingredient.name == ingredientName)
            {
                int riddleCount = ingredient.riddles.Length;
                int rIndex = Random.Range(0, riddleCount);
                string riddle = ingredient.riddles[rIndex];

                Instance.riddleTextBox.text = riddle;
                break;
            }
            iIndex++;
        }
    }
}