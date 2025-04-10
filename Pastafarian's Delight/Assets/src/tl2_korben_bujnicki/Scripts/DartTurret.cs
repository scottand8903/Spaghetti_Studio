using UnityEngine;

public class DartTurret : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform firePoint;
    public Vector2 fireDirection = Vector2.right; // Set in Inspector for direction (e.g., left, up)

    public void FireDart()
    {
        GameObject dart = Instantiate(dartPrefab, firePoint.position, Quaternion.identity);
        DartProjectile dartScript = dart.GetComponent<DartProjectile>();
        if (dartScript != null)
        {
            dartScript.SetDirection(fireDirection.normalized);
        }
    }
}
