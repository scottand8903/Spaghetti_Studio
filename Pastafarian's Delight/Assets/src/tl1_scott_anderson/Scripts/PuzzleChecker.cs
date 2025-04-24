using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PuzzleChecker : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
        // CheckIfCorrect();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CheckIfCorrect();
        }
    }

    public void CheckIfCorrect()
    {
        if(InventorySystem.Instance == null)
        {
            Debug.LogError("InventorySystem instance is null");
            return;
        }

        if(Puzzle.Instance.currentDish == null)
        {
            Debug.LogError("Current dish is null");
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
        Debug.Log("Puzzle complete.");

        // Clear the inventory
        InventorySystem.Instance.wipeInventory();

        SelectNewPuzzle();
    }

    private void SelectNewPuzzle()
    {
        Puzzle.Instance.currentDish = Puzzle.Instance.PickPastaDish();

        if(Puzzle.Instance.currentDish != null)
        {
            Debug.Log($"New puzzle selected: {Puzzle.Instance.currentDish.dishName}");

            GameStateManager.Instance.ResetAllRooms();
            GameStateManager.Instance.ResetCollectedIngredients();
        }
        else
        {
            Debug.LogError("No new puzzle was selected");
        }
    }
}
