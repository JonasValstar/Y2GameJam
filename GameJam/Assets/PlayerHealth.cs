using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    public int maxHP;
    public int pts;

    MainScript ms;

    void Start()
    {
        ms = Camera.main.GetComponent<MainScript>();
        ms.UpdatePlayerHpUI(hp, maxHP);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        ms.UpdatePlayerHpUI(hp, maxHP);
    }


    // testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            TakeDamage(5);
        }
    }
}
