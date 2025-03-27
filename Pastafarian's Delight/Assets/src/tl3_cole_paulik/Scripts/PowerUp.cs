using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public virtual void ApplyEffect(GameObject player)
    {
        Debug.Log("PowerUp applied to " + player.name);
    }
}
