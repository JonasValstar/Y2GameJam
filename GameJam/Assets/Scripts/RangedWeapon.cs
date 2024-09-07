using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedWeapon : MonoBehaviour
{
    /* --- variables --- */

    // firing variables
    float currentDelay = 0;
    int currentAmmo;
    MainScript ms;
    bool reloading = false;

    // stats
    public statsBase baseStats;
    public statsDamage damageStats;

    // components
    [SerializeField] Transform firePoint;
    [HideInInspector] public float shootForce;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip[] shootSounds = new AudioClip[4];
    [SerializeField] AudioClip reloadSound;

    /* --- functions --- */

    void Awake()
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
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
                newBullet.name = "bullet";

                // setting the stats
                shootForce = baseStats.bulletSpeed;
                bulletScript.damageStats = damageStats;

                //Shoot the bullet
                newBullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * shootForce, ForceMode.Impulse);

                // updating ui
                ms.UpdateAmmo(left, currentAmmo.ToString());

                // reapplying the delay
                currentDelay = baseStats.fireDelay;

                // consuming ammo
                currentAmmo--;

                // playing sound
                GetComponent<AudioSource>().clip = shootSounds[Random.Range(0, 4)];
                GetComponent<AudioSource>().Play();
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
            currentAmmo = 0;
            GetComponent<AudioSource>().clip = reloadSound;
            GetComponent<AudioSource>().Play();
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

    public void startAmmoUI(bool left)
    {
        ms.UpdateAmmo(left, currentAmmo.ToString());
    }

    /* --- Structs --- */

    // struct containing the basic stats
    [Serializable]
    public struct statsBase
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