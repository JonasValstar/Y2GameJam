using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] PlayerHealth player;
    int currentLevel = 0;

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
            GameObject[] items = new GameObject[3];
            for(int i = 0; i < 3; i++) {
                int index = Random.Range(0, Mathf.FloorToInt(player.pts/50) + 1);
                items[i] = storeItems[index][Random.Range(0, storeItems[index].Count)];
            }

            ms.ToggleStoreUI(true, items);
            canBuy = false;
        }

        if (Mathf.FloorToInt(player.pts/50) > currentLevel) {
            canBuy = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            canBuy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            canBuy = false;
        }
    }
}
