using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // variables
    public float speed;
    public RangedWeaponScript.statsDamage damageStats;

    // moving the bullet
    void Update() { 
        transform.Translate(speed * Time.deltaTime, 0, 0); 
    }

    // colliding with something
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") {
            other.GetComponent<EnemyBase>().TakeDamage(damageStats);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
