using UnityEngine;

/// <summary>
/// Represents a spawn point in the game where the player can be positioned
/// based on the last door used.
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    /// <summary>
    /// The name of the spawn point, used to match with the last door used.
    /// </summary>
    [SerializeField] private string spawnName;

    /// <summary>
    /// Called once before the first execution of Update after the MonoBehaviour is created.
    /// Checks if the spawn point matches the last door used and positions the player accordingly.
    /// </summary>
    void Start()
    {
        if(GameController.Instance != null && GameController.Instance.lastDoorUsed == spawnName)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                player.transform.position = transform.position;
                Debug.Log("player spawned at: " + spawnName);
            }
        }
    }
}
