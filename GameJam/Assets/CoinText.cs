using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void IncrementCoinCount(int coinTotal)
    {
        coinText.text = $"Coins: {coinTotal}";
    }
}
