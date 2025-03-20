using UnityEngine;

// Base class for power-ups
public abstract class PowerUp : MonoBehaviour
{
    public abstract void ApplyEffect(GameObject player);
}
