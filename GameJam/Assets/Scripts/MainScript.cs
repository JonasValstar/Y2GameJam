using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainScript : MonoBehaviour
{
    [Header("Damage Pop-ups")]
    [SerializeField] Transform uiCanvas;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject critTextPrefab;

    [Header("Ammo UI")]
    [SerializeField] TMP_Text ammoText0;
    [SerializeField] TMP_Text ammoText1;

    [Header("Score UI")]
    [SerializeField] TMP_Text TimerText;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text highScore;
    [HideInInspector] public int time = 260;
    public int maxTime = 260;

    [Header("Store UI")]
    [SerializeField] GameObject StoreOpenText;
    [SerializeField] Store storeScript;
    [SerializeField] GameObject allItems;
    [SerializeField] List<TMP_Text> names = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> baseDamage = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> elemDamage = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> maxAmmo = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> fireRate = new List<TMP_Text>();
    [HideInInspector] public bool inStore = false;

    void Start()
    {
        RangedWeapon[] empty = new RangedWeapon[0];
        ToggleStoreUI(false, empty);

        time = maxTime;
        StartCoroutine(scoreTimer());

        StoreOpenText.SetActive(false);
    }

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

    public void ToggleStoreUI(bool on, RangedWeapon[] items)
    {
        // activating or deactivating ui elements
        allItems.SetActive(on);

        // displaying items
        if (on) {
            for(int i = 0; i < items.Length; i++) {
                names[i].text = items[i].baseStats.name;
                baseDamage[i].text = items[i].damageStats.basic.ToString();
                maxAmmo[i].text = items[i].baseStats.maxAmmo.ToString();
                fireRate[i].text = Mathf.Round(60 / items[i].baseStats.fireDelay).ToString();
                if (items[i].damageStats.fire > 0) {elemDamage[i].text = items[i].damageStats.fire.ToString(); elemDamage[i].color = Color.red;}
                else if (items[i].damageStats.acid > 0) {elemDamage[i].text = items[i].damageStats.acid.ToString(); elemDamage[i].color = Color.green;}
                else if (items[i].damageStats.shock > 0) {elemDamage[i].text = items[i].damageStats.shock.ToString(); elemDamage[i].color = Color.yellow;}
                else if (items[i].damageStats.ice > 0) {elemDamage[i].text = items[i].damageStats.ice.ToString(); elemDamage[i].color = Color.cyan;}
                else {elemDamage[i].text = "--"; elemDamage[i].color = Color.white;}
            }
        }
    }

    public void StartStoreTimer()
    {
        StartCoroutine(storeOpen());
    }

    IEnumerator storeOpen()
    {
        StoreOpenText.SetActive(true);
        yield return new WaitForSeconds(3f);
        StoreOpenText.SetActive(false);
    }

    public void UpdateTimer()
    {
        int min = Mathf.FloorToInt(time/60);
        string sec = (time - min*60).ToString();
        if (sec.Length == 1) {sec = "0"+sec;}
        TimerText.text = $"{min}:{sec}";
    }

    IEnumerator scoreTimer()
    {
        yield return new WaitForSeconds(1f);
        time--;
        UpdateTimer();
        StartCoroutine(scoreTimer());
        if (time <= 0) { SceneManager.LoadScene(0);}
    }
}