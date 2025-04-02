using UnityEngine;

/// <summary>
/// Rotates the GameObject to face the mouse cursor position in the game world.
/// </summary>
public class RotateToMouse : MonoBehaviour
{
    /// <summary>
    /// Called once per frame to update the rotation of the GameObject.
    /// </summary>
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 to align the front
    }
}
