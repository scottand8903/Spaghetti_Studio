using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    public float fireCooldown = 2f;
    private float fireTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
    }

    public void TryFireAt(Transform target)
    {
        if(fireTimer <= 0f && target != null)
        {
            FireProjectile(target);
            fireTimer = fireCooldown;
        }
    }

    private void FireProjectile(Transform target)
    {
        if(target == null)
        {
            Debug.Log("Cannot Fire: Target is null");
            return;
        }
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = ((Vector2)target.position - (Vector2)firePoint.position).normalized;
            EnemyProjectile projectileScript = proj.GetComponent<EnemyProjectile>();
            Debug.Log(direction);
            projectileScript.SetDirection(direction);
            projectileScript.speed = projectileSpeed;
        }
    }
}
