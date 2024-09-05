using System;
using UnityEngine;
using Random=UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    // stats
    [SerializeField] statsBase baseStats;
    [SerializeField] statsResist resistStats;

    // taking damage
    public void TakeDamage(RangedWeaponScript.statsDamage damageStats) {

        // variables
        int damage = damageStats.basic;
        bool wasCrit = false;
        string element = "basic";

        // calculating damage
        if (resistStats.fire == resistType.noEffect) { damage += damageStats.fire; element = "fire"; } else if (resistStats.fire == resistType.Weakness) { damage += damageStats.fire*2; element = "fire"; }
        if (resistStats.acid == resistType.noEffect) { damage += damageStats.acid; element = "acid"; } else if (resistStats.acid == resistType.Weakness) { damage += damageStats.acid*2; element = "acid"; }
        if (resistStats.shock == resistType.noEffect) { damage += damageStats.shock; element = "shock"; } else if (resistStats.shock == resistType.Weakness) { damage += damageStats.shock*2; element = "shock"; }
        if (resistStats.blast == resistType.noEffect) { damage += damageStats.blast; element = "blast"; } else if (resistStats.blast == resistType.Weakness) { damage += damageStats.blast*2; element = "blast"; }
        if (Random.Range(0, 101) < baseStats.critChance) { damage = Mathf.RoundToInt(damage * 1.5f); wasCrit = true; }
        baseStats.health -= damage;

        // displaying popup
        Camera.main.GetComponent<MainScript>().DamagePopup(damage, wasCrit, element, transform);
    }

    /* --- Structs --- */

    // struct containing the base stats
    [Serializable] struct statsBase {
        public int health;
        public float moveSpeed;
        public int critChance;
    }

    [Serializable] struct statsResist {
        public resistType fire;
        public resistType acid;
        public resistType shock;
        public resistType blast;
    }

    enum resistType {
        noEffect,
        resistance,
        Weakness
    }
}
