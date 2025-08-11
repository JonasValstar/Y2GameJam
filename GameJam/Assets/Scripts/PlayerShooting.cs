using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerShooting : MonoBehaviour
{
    // variables
    [SerializeField] RangedWeapon weapon0;
    [SerializeField] RangedWeapon weapon1;
    [SerializeField] Transform weaponContainer;

    [SerializeField] GameObject[] testingWeaponPrefabs = new GameObject[4];

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (weapon0.FireBullet(true))
            {
                Debug.Log("Ammo is empty");
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (weapon1.FireBullet(false))
            {
                Debug.Log("Ammo is empty");
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            weapon0.Reload(true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            weapon1.Reload(false);
        }

        //! testing
        if (Input.GetKey(KeyCode.Z))
        {
            switchWeapon(true, testingWeaponPrefabs[0]);
        }
        if (Input.GetKey(KeyCode.X))
        {
            switchWeapon(true, testingWeaponPrefabs[1]);
        }
        if (Input.GetKey(KeyCode.C))
        {
            switchWeapon(true, testingWeaponPrefabs[2]);
        }
        if (Input.GetKey(KeyCode.V))
        {
            switchWeapon(true, testingWeaponPrefabs[3]);
        }

    }

    void Start() {
        weapon0.StartAmmoUI(true);
        weapon1.StartAmmoUI(false);
    }

    public void switchWeapon(bool left, GameObject weapon)
    {
        if (left)
        {
            GameObject newWeapon = Instantiate(weapon, weaponContainer);
            newWeapon.transform.position = weapon0.transform.position;
            newWeapon.transform.rotation = weapon0.transform.rotation;
            newWeapon.transform.localScale = weapon0.transform.localScale;
            Destroy(weapon0.gameObject);
            weapon0 = newWeapon.GetComponent<RangedWeapon>();
        } else {
            GameObject newWeapon = Instantiate(weapon, weaponContainer);
            newWeapon.transform.position = weapon1.transform.position;
            newWeapon.transform.rotation = weapon1.transform.rotation;
            newWeapon.transform.localScale = weapon1.transform.localScale;
            Destroy(weapon1.gameObject);
            weapon1 = newWeapon.GetComponent<RangedWeapon>();
        }

        Camera.main.GetComponent<MainScript>().UpdateAmmo(left, weapon.GetComponent<RangedWeapon>().baseStats.maxAmmo.ToString());
    }
}