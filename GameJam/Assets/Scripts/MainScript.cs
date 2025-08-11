using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainScript : MonoBehaviour
{
    [Header("Damage Pop-ups")]
    [SerializeField] Transform uiCanvas;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject critTextPrefab;

    [Header("Element Colours")]
    public Color fireColor;
    public Color acidColor;
    public Color shockColor;
    public Color iceColor;

    [Header("Ammo UI")]
    [SerializeField] TMP_Text ammoText0;
    [SerializeField] TMP_Text ammoText1;

    [Header("Score UI")]
    [SerializeField] TMP_Text TimerText;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text highScore;
    [HideInInspector] public int time = 600;
    public int maxTime = 600;

    [Header("Store UI")]
    [SerializeField] GameObject StoreOpenText;
    [SerializeField] Store storeScript;
    [SerializeField] GameObject allItems;
    [SerializeField] List<TMP_Text> names = new();
    [SerializeField] List<TMP_Text> baseDamage = new();
    [SerializeField] List<TMP_Text> elemDamage = new();
    [SerializeField] List<TMP_Text> maxAmmo = new();
    [SerializeField] List<TMP_Text> fireRate = new();
    [HideInInspector] public bool inStore = false;

    void Start()
    {
        RangedWeapon[] empty = new RangedWeapon[0];
        ToggleStoreUI(false, empty);

        time = maxTime;
        StartCoroutine(ScoreTimer());

        StoreOpenText.SetActive(false);

        EventManager.AddListener<PlayerHitEvent>(DecreaseScoreTimer);
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

        // Element colour switch
        Color textColour;
        textColour = element.ToLower() switch
        {
            "fire" => fireColor,
            "acid" => acidColor,
            "shock" => shockColor,
            "ice" => iceColor,
            _ => Color.white,
        };
        damage.GetComponent<TMP_Text>().color = textColour;
    }

    public void UpdateAmmo(bool left, string ammo)
    {
        if (left)
        {
            if (ammo == "0")
            {
                ammo = " " + "0 [Q]";
            }
            ammoText0.text = ammo;
        }
        else
        {
            if (ammo == "0")
            {
                ammo = "0 [E]" + " ";
            }
            ammoText1.text = ammo.ToString();
        }
    }

    public void ToggleStoreUI(bool on, RangedWeapon[] items)
    {
        // activating or deactivating ui elements
        allItems.SetActive(on);

        // displaying items
        if (on)
        {
            for (int i = 0; i < items.Length; i++)
            {
                names[i].text = items[i].baseStats.name;
                baseDamage[i].text = items[i].damageStats.basic.ToString();
                maxAmmo[i].text = items[i].baseStats.maxAmmo.ToString();
                fireRate[i].text = Mathf.Round(60 / items[i].baseStats.fireDelay).ToString();
                if (items[i].damageStats.fire > 0) { elemDamage[i].text = items[i].damageStats.fire.ToString(); elemDamage[i].color = Color.red; }
                else if (items[i].damageStats.acid > 0) { elemDamage[i].text = items[i].damageStats.acid.ToString(); elemDamage[i].color = Color.green; }
                else if (items[i].damageStats.shock > 0) { elemDamage[i].text = items[i].damageStats.shock.ToString(); elemDamage[i].color = Color.yellow; }
                else if (items[i].damageStats.ice > 0) { elemDamage[i].text = items[i].damageStats.ice.ToString(); elemDamage[i].color = Color.cyan; }
                else { elemDamage[i].text = "--"; elemDamage[i].color = Color.white; }
            }
        }
    }

    public void StartStoreTimer()
    {
        StartCoroutine(StoreOpen());
    }

    IEnumerator StoreOpen()
    {
        StoreOpenText.SetActive(true);
        yield return new WaitForSeconds(3f);
        StoreOpenText.SetActive(false);
    }

    public void UpdateTimer()
    {
        int min = Mathf.FloorToInt(time / 60);
        string sec = (time - min * 60).ToString();
        if (sec.Length == 1) { sec = "0" + sec; }
        TimerText.text = $"{min}:{sec}";
    }

    IEnumerator ScoreTimer()
    {
        yield return new WaitForSeconds(1f);
        time--;
        UpdateTimer();
        StartCoroutine(ScoreTimer());
        if (time <= 0) { SceneManager.LoadScene(0); }
    }
    public float GetGameTimerElapsedTime()
    {
        return time / maxTime;
    }
    private void DecreaseScoreTimer(PlayerHitEvent evt)
    {
        // Ensure time doesn't go below zero
        time = Mathf.Max(0, time - evt.dmg);
        UpdateTimer(); // Update the UI after decreasing time

        // If time runs out, trigger the end game
        if (time <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene(0); }
    }
}
