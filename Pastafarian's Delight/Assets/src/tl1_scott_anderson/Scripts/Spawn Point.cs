using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private string spawnName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
