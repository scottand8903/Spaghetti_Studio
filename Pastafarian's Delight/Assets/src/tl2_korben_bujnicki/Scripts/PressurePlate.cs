using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DartTurret connectedTurret; // Reference to the turret to fire

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (connectedTurret != null)
            {
                connectedTurret.FireDart();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false; // Allow re-triggering if desired
        }
    }
}
