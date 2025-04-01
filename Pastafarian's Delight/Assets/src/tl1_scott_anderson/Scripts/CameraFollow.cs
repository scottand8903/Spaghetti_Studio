using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign Player in the Inspector
    public float smoothSpeed = 100f;

    void LateUpdate()
    {
        if (player == null) return;

        // Follow the player's position, but don't rotate with it
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }
}
