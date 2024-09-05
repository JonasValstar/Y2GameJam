using UnityEngine;

public class Bullet : MonoBehaviour
{
    // variables
    [HideInInspector] public float speed;
    [HideInInspector] public RangedWeapon.statsDamage damageStats;

    // moving the bullet
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    // colliding with something
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyBase>().TakeDamage(damageStats);
            Destroy(gameObject);
        }
        else if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}