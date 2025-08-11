using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    // stats
    [SerializeField] StatsBase baseStats;
    [SerializeField] ElementType elementType;
    [SerializeField] StatsResist resistStats;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] AudioClip[] dyingSounds = new AudioClip[3];
    public static event Action OnEnemyKilled;

    bool dying = false;

    private MainScript mainScript;

    private void Start()
    {
        Camera.main.TryGetComponent<MainScript>(out mainScript);
    }

    void Update()
    {
        if (baseStats.health <= 0 && !dying)
        {
            StartCoroutine(OnDeath());
            dying = true;
        }
    }
    private IEnumerator OnDeath()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        agent.height = .5f;

        GameObject instantiatedObject;
        if (gameObject.CompareTag("Spitter"))
        {
            instantiatedObject = Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 2.8f, transform.position.z), Quaternion.identity);
        }
        else
        {
            instantiatedObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        //play sound
        Camera.main.GetComponent<AudioSource>().clip = dyingSounds[Random.Range(0, 3)];
        Camera.main.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);

        OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }

    // taking damage
    public void TakeDamage(RangedWeapon.StatsDamage damageStats)
    {
        // variables
        float damage = damageStats.basic * Random.Range(0.9f, 1.1f);
        bool wasCrit = false;
        string element = "basic";
        float randMult = Random.Range(0.9f, 1.1f);

        // calculating damage
        if (damageStats.fire > 0) { if (resistStats.fire == ResistType.noEffect) { damage += damageStats.fire * randMult; element = "fire"; } else if (resistStats.fire == ResistType.Weakness) { damage += damageStats.fire * 2 * randMult; element = "fire"; } }
        if (damageStats.acid > 0) { if (resistStats.acid == ResistType.noEffect) { damage += damageStats.acid * randMult; element = "acid"; } else if (resistStats.acid == ResistType.Weakness) { damage += damageStats.acid * 2 * randMult; element = "acid"; } }
        if (damageStats.shock > 0) { if (resistStats.shock == ResistType.noEffect) { damage += damageStats.shock * randMult; element = "shock"; } else if (resistStats.shock == ResistType.Weakness) { damage += damageStats.shock * 2 * randMult; element = "shock"; } }
        if (damageStats.ice > 0) { if (resistStats.ice == ResistType.noEffect) { damage += damageStats.ice * randMult; element = "ice"; } else if (resistStats.ice == ResistType.Weakness) { damage += damageStats.ice * 2 * randMult; element = "ice"; } }
        if (Random.Range(0, 101) < baseStats.critChance) { damage *= 1.5f; wasCrit = true; }

        baseStats.health -= (int)damage;

        Debug.Log(element);

        // displaying popup
        mainScript.DamagePopup((int)damage, wasCrit, element, transform);
    }

    /* --- Structs --- */

    // struct containing the base stats
    [Serializable]
    struct StatsBase
    {
        public int health;
        public int critChance;
    }

    [Serializable]
    struct StatsResist
    {
        public ResistType fire;
        public ResistType acid;
        public ResistType shock;
        public ResistType ice;
    }

    enum ResistType
    {
        noEffect,
        resistance,
        Weakness
    }

    enum ElementType
    {
        Water,
        Fire,
        Acid,
        Shock,
    }
}