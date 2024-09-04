using System;
using UnityEngine;

public class RangedWeaponScript : MonoBehaviour
{
    /* --- variables --- */

    // firing variables
    int ammo;
    float currentDelay = 0;

    // stats
    [SerializeField] statsBase baseStats;
    [SerializeField] statsDamage damageStats;

    // components
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    /* --- functions --- */

    // Update is called once per frame
    void Update()
    {
        //! move into player script at some point
        // checking if gun can fire
        if (currentDelay <= 0) {
            // firing a bullet if needed
            if (Input.GetKey(KeyCode.Mouse0)) {
                FireBullet();
            }
            
            // setting the delay
            currentDelay = baseStats.fireDelay;
        } else {
            currentDelay -= Time.deltaTime;
        }
    }

    // firing a bullet
    void FireBullet()
    {
        // making the bullet
        GameObject newBullet = Instantiate(bulletPrefab);
        BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
        newBullet.transform.position = firePoint.position;

        // setting the stats
        bulletScript.speed = baseStats.bulletSpeed;
        bulletScript.damageStats = damageStats;
    }

    /* --- Structs --- */

    // struct containing the basic stats
    [Serializable] struct statsBase {
        public int maxAmmo;
        public float fireDelay;
        public float bulletSpeed;
    }
    
    // struct containing the stats related to damage dealt
    [Serializable] public struct statsDamage {
        public int basic;
        public int fire;
        public int acid;
        public int shock;
        public int blast;
    }
}