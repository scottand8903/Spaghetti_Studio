using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PuzzleChecker : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfCorrect();
    }

    public void CheckIfCorrect()
    {
        if(InventorySystem.Instance == null)
        {
            Debug.Log("InventorySystem instance is null");
            return;
        }

        if(Puzzle.Instance.currentDish == null)
        {
            Debug.Log("Current dish is null");
            return;
        }

        int[] inventory = InventorySystem.Instance.GetInventoryIDs();
        List<int> correctIngredients = Puzzle.Instance.currentDish.ingredients.Select(ing => ing.id).ToList();

        for(int i = 0; i < 3; i++)
        {
            int itemID = inventory[i];
            if (!correctIngredients.Contains(itemID))
            {
                Debug.Log($"Slot {i + 1} contains an incorrect ingredient: {itemID}");
                Debug.Log("Not all ingredients match.");
                return;
            }
        }

        // If all 3 slots match
        Debug.Log("All 3 ingredients in the first 3 slots match the current dish!");
        ConfirmMatch();
    }

    private void ConfirmMatch()
    {
        Debug.Log("Puzzle complete.");
    }

}
