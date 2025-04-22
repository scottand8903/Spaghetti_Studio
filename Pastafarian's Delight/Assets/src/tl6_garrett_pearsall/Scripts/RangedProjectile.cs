using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    
    public GameObject projectile;
    public Transform attackPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Instantiate(projectile, attackPoint.position, transform.rotation);
    }
}
