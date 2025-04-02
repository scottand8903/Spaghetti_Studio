using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of pasta dishes and provides functionality to pick a random dish.
/// </summary>
public class PuzzleManager
{
    // A list to store the available pasta dishes.
    protected List<PastaDish> pastaDishes = new List<PastaDish>();

    /// <summary>
    /// Constructor to initialize the PuzzleManager with an array of pasta dishes.
    /// </summary>
    /// <param name="dishes">An array of PastaDish objects to initialize the manager.</param>
    public PuzzleManager(PastaDish[] dishes)
    {
        if (dishes != null)
        {
            // Add all provided dishes to the internal list.
            pastaDishes.AddRange(dishes);
        }
    }

    /// <summary>
    /// Picks a random pasta dish from the available dishes.
    /// </summary>
    /// <returns>A randomly selected PastaDish object, or null if no dishes are available.</returns>
    public virtual PastaDish PickPastaDish()
    {
        if (pastaDishes.Count > 0)
        {
            // Return a random dish from the list.
            return pastaDishes[Random.Range(0, pastaDishes.Count)];
        }
        else
        {
            // Log an error if no dishes are available and return null.
            Debug.LogError("No dishes available");
            return null;
        }
    }
}
