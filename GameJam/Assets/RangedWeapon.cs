using System;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    /* --- variables --- */

    // firing variables
    float currentDelay = 0;
    int currentAmmo;
    MainScript ms;

    // stats
    [SerializeField] statsBase baseStats;
    [SerializeField] statsDamage damageStats;

    // components
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    /* --- functions --- */

    void Start()
    {
        // setting the ammo to max
        currentAmmo = baseStats.maxAmmo;
        ms = Camera.main.GetComponent<MainScript>();

    }

    // firing a bullet
    public bool FireBullet(bool left)
    {
        if (currentAmmo > 0)
        {
            if (currentDelay < 0)
            {
                // making the bullet
                GameObject newBullet = Instantiate(bulletPrefab);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
                newBullet.transform.position = firePoint.position;
                newBullet.transform.rotation = firePoint.rotation;
                newBullet.name = "bullet";

                // setting the stats
                bulletScript.speed = baseStats.bulletSpeed;
                bulletScript.damageStats = damageStats;

                // updating ui
                ms.UpdateAmmo(left, currentAmmo);

                // reapplying the delay
                currentDelay = baseStats.fireDelay;

                // consuming ammo
                currentAmmo--;
            }
            else
            {
                // decreasing current wait time
                currentDelay -= Time.deltaTime;
            }

            // returning ammo is not empty
            return false;
        }
        else
        {
            // updating ui
            ms.UpdateAmmo(left, 0);

            // returning that ammo is empty
            return true;

        }
    }

    public void Reload()
    {
        currentAmmo = baseStats.maxAmmo;
    }

    /* --- Structs --- */

    // struct containing the basic stats
    [Serializable]
    struct statsBase
    {
        public int maxAmmo;
        public float fireDelay;
        public float bulletSpeed;
    }

    // struct containing the stats related to damage dealt
    [Serializable]
    public struct statsDamage
    {
        public int basic;
        public int fire;
        public int acid;
        public int shock;
        public int ice;
    }
}