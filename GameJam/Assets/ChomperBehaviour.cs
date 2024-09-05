using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ChomperBehaviour : MonoBehaviour
{
    #region VARIABLES
    [Header("References")]
    private GameObject player;
    private GameObject enemyTarget;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public GameObject floatingText;
    public AudioSource enemyAudioHurt;
    public LayerMask whatIsGround, whatIsTarget;

    [Header("Variables")]
    public float m_MaxHealth;
    public float m_CurrentHealth;
    public bool m_IsDead = false;
    //private bool m_IsStunned;

    [SerializeField] private float m_ChaseSpeed;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public BoxCollider chompAttack;

    [Header("States")]
    public EnemyState state;
    [Space(5)]
    public float attackRange;
    public bool targetInAttackRange;

    public enum EnemyState
    {
        chasing,
        attacking
    }
    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        whatIsTarget = LayerMask.GetMask("Player");
        player = GameObject.FindWithTag("Player").transform.root.gameObject;
        enemyTarget = player;
    }

    private void Update()
    {
        if (!m_IsDead)
        {
            //Check for attack range
            targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);

            if (!targetInAttackRange) ChaseTarget();
            if (targetInAttackRange) AttackTarget();
        }

        if (m_IsDead)
        {

        }
    }

    #region STATE FUNCTIONS
    private void ChaseTarget()
    {
        state = EnemyState.chasing;
        agent.speed = m_ChaseSpeed;

        transform.LookAt(enemyTarget.transform);
        SetAgentDestination(enemyTarget.transform.position);
    }

    private void AttackTarget()
    {
        state = EnemyState.attacking;

        Debug.Log("Attacking player");

        //Make sure enemy doesn't move
        SetAgentDestination(transform.position);

        transform.LookAt(enemyTarget.transform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            chompAttack.enabled = true;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        chompAttack.enabled = false;
        alreadyAttacked = false;
    }

    private void SetAgentDestination(Vector3 destination)
    {
        if (agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(destination);
        }
    }
    #endregion

    #region HEALTH & DMG FUNCTIONS
    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called with {damage} damage.");
        if (damage <= 0) return;

        m_CurrentHealth -= damage;

        //Play hurt sound effect
        enemyAudioHurt.Play();

        //Trigger floating text
        if (floatingText && m_CurrentHealth > 0)
            ShowFloatingText($"{damage}");

        if (m_CurrentHealth <= 0)
        {
            m_IsDead = true;
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void ShowFloatingText(string textToShow)
    {
        //Can be optimized by Object Pooling
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = textToShow;
    }

    #endregion

    #region GIZMOS
    /// <summary>
    /// Visualize attack and sight range.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion
}
