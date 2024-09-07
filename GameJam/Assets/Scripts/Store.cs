using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Store : MonoBehaviour
{
    // items
    List<List<GameObject>> storeItems = new List<List<GameObject>>();
    [SerializeField] List<GameObject> level0 = new List<GameObject>();
    [SerializeField] List<GameObject> level1 = new List<GameObject>();
    [SerializeField] List<GameObject> level2 = new List<GameObject>();
    [SerializeField] List<GameObject> level3 = new List<GameObject>();
    [SerializeField] List<GameObject> level4 = new List<GameObject>();

    // components
    MainScript ms;
    [SerializeField] PlayerShooting player;
    [SerializeField] GameHandler handler;
    int currentLevel = 0;
    RangedWeapon[] items = new RangedWeapon[3];


    // variables
    bool canBuy = false;

    // Start is called before the first frame update
    void Start()
    {
        // fuck off, its a game jam...
        storeItems.Add(level0);
        storeItems.Add(level1);
        storeItems.Add(level2);
        storeItems.Add(level3);
        storeItems.Add(level4);

        // getting components
        ms = Camera.main.GetComponent<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canBuy) {
            // getting the items
            for(int i = 0; i < 3; i++) {
                int index = Random.Range(0, Mathf.Clamp(Mathf.FloorToInt(handler.coinCount/15), 0, 5));
                items[i] = storeItems[index][Random.Range(0, storeItems[index].Count)].GetComponent<RangedWeapon>();
            }

            ms.ToggleStoreUI(true, items);
            canBuy = false;
        }

        if (Mathf.FloorToInt(handler.coinCount/15) > currentLevel) {
            canBuy = true;
            currentLevel++;
            ms.StartStoreTimer();
        }
    }

    public void applyPurchase(int option) {
        if (option == 0) { player.switchWeapon(true, items[0].gameObject); }
        if (option == 1) { player.switchWeapon(false, items[0].gameObject); }
        if (option == 2) { player.switchWeapon(true, items[1].gameObject); }
        if (option == 3) { player.switchWeapon(false, items[1].gameObject); }
        if (option == 4) { player.switchWeapon(true, items[2].gameObject); }
        if (option == 5) { player.switchWeapon(false, items[2].gameObject); }

        RangedWeapon[] empty = new RangedWeapon[0];
        ms.ToggleStoreUI(false, empty);
    }
}
