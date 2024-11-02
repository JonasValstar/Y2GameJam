using UnityEngine;

public class SpitterBehaviour : EnemyBehaviour
{
    #region VARIABLES
    [Header("Attacking")]
    [SerializeField] 
    private GameObject projectile;
    #endregion

    public override void AttackTarget()
    {
        base.AttackTarget();

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 2.8f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
}
