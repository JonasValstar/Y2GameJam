using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void IncrementCoinCount(int coinTotal)
    {
        coinText.text = $"{coinTotal}";
        PlayerPrefs.SetInt("LastScore", coinTotal);
    }

    void Start()
    {
        IncrementCoinCount(0);
    }
}
