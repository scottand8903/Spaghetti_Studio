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
