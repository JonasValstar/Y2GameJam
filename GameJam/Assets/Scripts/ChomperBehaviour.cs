using UnityEngine;

public class ChomperBehaviour : EnemyBehaviour
{
    #region VARIABLES
    [Header("Attacking")]
    [SerializeField]
    private BoxCollider chompAttack;

    private bool hasCollided = false; // Track if a collision has occurred
    #endregion

    public override void AttackTarget()
    {
        base.AttackTarget();

        if (!alreadyAttacked)
        {
            chompAttack.enabled = true;
            alreadyAttacked = true;
            hasCollided = false; // Reset collision for new attack
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public override void ResetAttack()
    {
        chompAttack.enabled = false;
        alreadyAttacked = false;
        hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger only if the collider is the player and has not already collided
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            PlayerHitEvent evt = Events.PlayerHitEvent;
            evt.dmg = dmg;
            EventManager.Broadcast(evt);

            Debug.Log("Player hit by Chomper, event broadcasted with dmg: " + dmg);
        }
    }
}
