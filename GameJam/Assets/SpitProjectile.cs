using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == CompareTag("Player"))
        {
            if (!Camera.main.GetComponent<MainScript>().inStore) {
                Camera.main.GetComponent<MainScript>().time-=10;
                Camera.main.GetComponent<MainScript>().UpdateTimer();
                //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
