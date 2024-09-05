using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
