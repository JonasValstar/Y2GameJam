using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    // variables
    [SerializeField] Transform uiCanvas;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject critTextPrefab;

    // display the amount of damage
    public void DamagePopup(int amount, bool crit, string element, Transform enemy)
    {
        // creating the text
        GameObject damage;
        if (crit) { damage = Instantiate(critTextPrefab, uiCanvas); } 
        else { damage = Instantiate(damageTextPrefab, uiCanvas); }

        // moving into the correct position
        damage.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(enemy.position);

        // setting the damage things
        damage.GetComponent<TMP_Text>().text = amount.ToString();
    }
}
