using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string doorName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("something entered the trigger: " + other.name);

        if(other.CompareTag("Player"))
        {
            Debug.Log("player entered : " + doorName);

            // hold onto previous door entered
            GameController.Instance.lastDoorUsed = doorName;
            // load new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
