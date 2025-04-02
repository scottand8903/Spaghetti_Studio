using UnityEngine;

// This script makes the camera follow the player smoothly without rotating with the player.
public class CameraFollow : MonoBehaviour
{
    // Reference to the player's Transform component. Assign this in the Unity Inspector.
    public Transform player;

    // Speed at which the camera follows the player. Higher values result in faster movement.
    public float smoothSpeed = 100f;

    // LateUpdate is called after all Update methods have been called. This ensures the camera follows the player
    // after the player has moved in the current frame.
    void LateUpdate()
    {
        // If the player Transform is not assigned, exit the method.
        if (player == null) return;

        // Calculate the new position for the camera. The camera follows the player's x and y position,
        // but retains its own z position to avoid changing the depth.
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Smoothly interpolate the camera's position from its current position to the new position.
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }
}
