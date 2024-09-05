using System.Collections;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class SpitterBehaviour : MonoBehaviour
{
    #region VARIABLES
    [Header("References")]
    private GameObject player;
    private GameObject enemyTarget;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public AudioSource enemyAudioHurt;
    public LayerMask whatIsGround, whatIsTarget;
    public GameObject deathEffect;

    [Header("Variables")]
    public float m_MaxHealth;
    public float m_CurrentHealth;

    [SerializeField] private float m_ChaseSpeed;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectile;

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
        //Check for attack range
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);

        if (!targetInAttackRange) ChaseTarget();
        if (targetInAttackRange) AttackTarget();
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
            Rigidbody rb = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 2.8f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
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
