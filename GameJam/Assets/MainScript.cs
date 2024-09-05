using TMPro;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [Header("Damage Pop-ups")]
    [SerializeField] Transform uiCanvas;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject critTextPrefab;

    [Header("Damage Pop-ups")]
    [SerializeField] TMP_Text ammoText0;
    [SerializeField] TMP_Text ammoText1;

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

    public void UpdateAmmo(bool left, int ammo)
    {
        if (left) { ammoText0.text = $"Ammo:\n{ammo}"; }
        else { ammoText1.text = $"Ammo:\n{ammo}"; }
    }
}