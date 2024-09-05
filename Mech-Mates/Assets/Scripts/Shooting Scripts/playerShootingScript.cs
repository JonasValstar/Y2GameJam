using UnityEngine;

public class playerShootingScript : MonoBehaviour
{
    // variables
    [SerializeField] RangedWeaponScript weapon0;
    [SerializeField] RangedWeaponScript weapon1;

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
    }
}
