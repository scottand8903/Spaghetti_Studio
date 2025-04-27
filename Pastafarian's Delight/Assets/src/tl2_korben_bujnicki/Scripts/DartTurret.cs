using UnityEngine;

//public class DartTurret : MonoBehaviour //comment out for dynamic
public class DartTurret:Trap
{
    public GameObject dartPrefab;
    public Transform firePoint;
    public Vector2 fireDirection = Vector2.right; // Set in Inspector for direction (e.g., left, up)
    
    //comment in for dynamic binding:
    public override void ActivateTrap()
    {
        Debug.Log("DartTurret activated!");
        FireDart();
    }

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
