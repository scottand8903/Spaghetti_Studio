using UnityEngine;

public class Weapon : MonoBehaviour
{
    public MeleeAttack meleeAttack;

    public GameObject projectile;
    public Transform attackPoint;

    public float attackRate = 2.0f;
    float nextAttackTime = 0f;

    public float offset;

    public float shotRate = 0.5f;
    float nextShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Time.time >= nextShotTime)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Instantiate(projectile, attackPoint.position, transform.rotation);
                nextShotTime = Time.time + 1f / shotRate;
            }
        }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                meleeAttack.Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
}
