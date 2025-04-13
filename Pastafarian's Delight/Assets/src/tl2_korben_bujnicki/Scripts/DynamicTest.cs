using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    // Abstract method to be overridden by specific traps
    public abstract void ActivateTrap();

    // Optional: Shared functionality across traps
    protected void CommonTrapBehavior()
    {
        Debug.Log("Common trap behavior executed.");
    }
}