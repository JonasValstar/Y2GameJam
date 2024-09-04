using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // variables
    public float speed;
    public RangedWeaponScript.statsDamage damageStats;

    // moving the bullet
    void Update() { 
        transform.Translate(speed * Time.deltaTime, 0, 0); 
    }
}
