using System;
using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    /* --- variables --- */

    // firing variables
    float currentDelay = 0;
    int currentAmmo;
    MainScript ms;
    bool reloading = false;

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
        if (currentAmmo > 0) {
            if (currentDelay < 0) {
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
                ms.UpdateAmmo(left, currentAmmo.ToString());

                // reapplying the delay
                currentDelay = baseStats.fireDelay;

                // consuming ammo
                currentAmmo--;
            } else {
                // decreasing current wait time
                currentDelay -= Time.deltaTime;
            }

            // returning ammo is not empty
            return false;
        } else {
            // updating ui
            if (!reloading) {ms.UpdateAmmo(left, "0"); }
 
            // returning that ammo is empty
            return true;
        }
    }

    public void Reload(bool left)
    {
        if (!reloading) {
            StartCoroutine(ReloadWait(left));
        }
        
    }

    IEnumerator ReloadWait(bool left)
    {
        reloading = true;
        ms.UpdateAmmo(left, "REL...");
        for(int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(0.5f);
            if (i % 2 == 0) { ms.UpdateAmmo(left, "REL.."); }
            else { ms.UpdateAmmo(left, "REL..."); }
        }
        currentAmmo = baseStats.maxAmmo;
        ms.UpdateAmmo(left, currentAmmo.ToString());
        reloading = false;
    }

    /* --- Structs --- */

    // struct containing the basic stats
    [Serializable]
    struct statsBase
    {
        public string name;
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