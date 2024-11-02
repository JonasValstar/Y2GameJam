using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region VARIABLES
    [Header("References")]
    [SerializeField]
    private SceneTypeObject ST_Player;

    protected GameObject player;

    protected GameObject enemyTarget;

    [SerializeField]
    protected Rigidbody rb;

    protected NavMeshAgent agent;

    [SerializeField]
    protected AudioSource enemyAudioHurt;

    protected LayerMask whatIsGround, whatIsTarget;

    [SerializeField]
    private GameObject deathEffect;

    [Header("Variables")]
    //[SerializeField]
    //protected float m_MaxHealth;
                                        // GP GETS HANDLED IN THE ENEMYBASE SCRIPT
    //[SerializeField]
    //protected float m_CurrentHealth;

    [SerializeField]
    protected float m_ChaseSpeed;

    [SerializeField]
    protected int dmg;

    [Header("Attacking")]
    [SerializeField]
    protected float timeBetweenAttacks;

    protected bool alreadyAttacked;

    [Header("States")]
    [SerializeField]
    protected EnemyState state;

    [Space(5)]

    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected bool targetInAttackRange;

    public enum EnemyState
    {
        chasing,
        attacking
    }
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        whatIsTarget = LayerMask.GetMask("Player");
        whatIsGround = LayerMask.GetMask("Ground");
        player = ST_Player.Objects[0];
        enemyTarget = player;
    }

    private void Update()
    {
        //Check for attack range
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);

        if (!targetInAttackRange) ChaseTarget();
        if (targetInAttackRange) AttackTarget();
    }
    #endregion

    #region STATE FUNCTIONS
    public void ChaseTarget()
    {
        state = EnemyState.chasing;
        agent.speed = m_ChaseSpeed;

        transform.LookAt(enemyTarget.transform);
        SetAgentDestination(enemyTarget.transform.position);
    }

    public virtual void AttackTarget()
    {
        state = EnemyState.attacking;

        Debug.Log("Attacking player");

        //Make sure enemy doesn't move
        SetAgentDestination(transform.position);

        transform.LookAt(enemyTarget.transform);

        //if (!alreadyAttacked)
        //{
            ///Attack code here
        //}
    }
    public virtual void ResetAttack()
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
