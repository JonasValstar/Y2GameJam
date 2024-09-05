using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [Header("Damage Pop-ups")]
    [SerializeField] Transform uiCanvas;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject critTextPrefab;

    [Header("Ammo UI")]
    [SerializeField] TMP_Text ammoText0;
    [SerializeField] TMP_Text ammoText1;

    [Header("Health UI")]
    [SerializeField] TMP_Text healthText;

    [Header("Store UI")]
    [SerializeField] List<GameObject> allItems = new List<GameObject>();
    [SerializeField] List<TMP_Text> names = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> baseDamage = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> elemDamage = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> maxAmmo = new List<TMP_Text>();

    // display the amount of damage
    public void DamagePopup(int amount, bool crit, string element, Transform enemy)
    {
        // creating the text
        GameObject damage;
        if (crit) { damage = Instantiate(critTextPrefab, uiCanvas); }
        else { damage = Instantiate(damageTextPrefab, uiCanvas); }

        // moving into the correct position
        damage.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(enemy.position);
        damage.GetComponent<RectTransform>().Translate(Random.Range(-10, 11), Random.Range(-10, 11) + 25, 0);

        // setting the damage things
        damage.GetComponent<TMP_Text>().text = amount.ToString();
    }

    public void UpdateAmmo(bool left, string ammo)
    {
        if (left) { ammoText0.text = ammo; }
        else { ammoText1.text = ammo.ToString(); }
    }

    public void ToggleStoreUI(bool on, GameObject[] items)
    {
        // activating or deactivating ui elements
        foreach (GameObject item in allItems) {
            item.SetActive(on);
        }

        // displaying items
        if (on) {
            for(int i = 0; i < items.Length; i++) {

            }
        }
    }

    public void UpdatePlayerHpUI(int hp, int maxHP)
    {
        healthText.text = $"{hp} / {maxHP}";
    }

    void Start()
    {
        GameObject[] empty = new GameObject[0];
        ToggleStoreUI(false, empty);
    }
}