using UnityEngine;

public class playerShootingScript : MonoBehaviour
{
    // variables
    [SerializeField] RangedWeaponScript weapon0;
    [SerializeField] RangedWeaponScript weapon1;

    [SerializeField] GameObject[] testingWeaponPrefabs = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (weapon0.FireBullet(true)) {
                Debug.Log("Ammo is empty");
            }
        } if (Input.GetKey(KeyCode.Mouse1)) {
            if (weapon1.FireBullet(false)) {
                Debug.Log("Ammo is empty");
            }
        }

        if (Input.GetKey(KeyCode.Q)) {
            weapon0.Reload();
        } if (Input.GetKey(KeyCode.E)) {
            weapon1.Reload();
        }

        //! testing
        if (Input.GetKey(KeyCode.Z)) {
            switchWeapon(true, testingWeaponPrefabs[0]);
        }if (Input.GetKey(KeyCode.X)) {
            switchWeapon(true, testingWeaponPrefabs[1]);
        }if (Input.GetKey(KeyCode.C)) {
            switchWeapon(true, testingWeaponPrefabs[2]);
        }if (Input.GetKey(KeyCode.V)) {
            switchWeapon(true, testingWeaponPrefabs[3]);
        }
        
    }

    void switchWeapon(bool left, GameObject weapon)
    {
        if (left) {
            GameObject newWeapon = Instantiate(weapon, transform);
            newWeapon.transform.position = weapon0.transform.position;
            Destroy(weapon0.gameObject);
            weapon0 = newWeapon.GetComponent<RangedWeaponScript>();
        }
    }
}
