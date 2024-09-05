using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    // stats
    [SerializeField] statsBase baseStats;
    [SerializeField] statsResist resistStats;

    void Update()
    {
        if (baseStats.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // taking damage
    public void TakeDamage(RangedWeapon.statsDamage damageStats)
    {

        // variables
        float damage = damageStats.basic * Random.Range(0.9f, 1.1f);
        bool wasCrit = false;
        string element = "basic";
        float randMult = Random.Range(0.9f, 1.1f);

        // calculating damage
        if (damageStats.fire > 0) { if (resistStats.fire == resistType.noEffect) { damage += damageStats.fire * randMult; element = "fire"; } else if (resistStats.fire == resistType.Weakness) { damage += damageStats.fire * 2 * randMult; element = "fire"; } }
        if (damageStats.acid > 0) { if (resistStats.acid == resistType.noEffect) { damage += damageStats.acid * randMult; element = "acid"; } else if (resistStats.acid == resistType.Weakness) { damage += damageStats.acid * 2 * randMult; element = "acid"; } }
        if (damageStats.shock > 0) { if (resistStats.shock == resistType.noEffect) { damage += damageStats.shock * randMult; element = "shock"; } else if (resistStats.shock == resistType.Weakness) { damage += damageStats.shock * 2 * randMult; element = "shock"; } }
        if (damageStats.ice > 0) { if (resistStats.ice == resistType.noEffect) { damage += damageStats.ice * randMult; element = "ice"; } else if (resistStats.ice == resistType.Weakness) { damage += damageStats.ice * 2 * randMult; element = "ice"; } }
        if (Random.Range(0, 101) < baseStats.critChance) { damage *= 1.5f; wasCrit = true; }

        baseStats.health -= (int)damage;

        Debug.Log(element);

        // displaying popup
        Camera.main.GetComponent<MainScript>().DamagePopup((int)damage, wasCrit, element, transform);
    }

    /* --- Structs --- */

    // struct containing the base stats
    [Serializable]
    struct statsBase
    {
        public int health;
        public int critChance;
    }

    [Serializable]
    struct statsResist
    {
        public resistType fire;
        public resistType acid;
        public resistType shock;
        public resistType ice;
    }

    enum resistType
    {
        noEffect,
        resistance,
        Weakness
    }
}