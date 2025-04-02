using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a door in the game that allows the player to transition between scenes.
/// </summary>
public class Door : MonoBehaviour
{
    // The name of the scene to load when the player interacts with this door.
    [SerializeField] private string sceneToLoad;

    // The unique name of this door, used to track which door the player used.
    [SerializeField] private string doorName;

    /// <summary>
    /// Triggered when another collider enters this door's trigger collider.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player.
        if(other.CompareTag("Player"))
        {
            Debug.Log("player entered : " + doorName);

            // Store the name of the last door used in the GameController.
            GameController.Instance.lastDoorUsed = doorName;

            // Load the specified scene.
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
