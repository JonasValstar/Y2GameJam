using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] CoinText coinText;
    [SerializeField] TextMeshProUGUI highScoreText;
    /*[HideInInspector]*/ public int coinCount;
    private void OnEnable()
    {
        EnemyBase.OnEnemyKilled += HandleScoreChange;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyKilled -= HandleScoreChange;
    }

    private void Start()
    {
        UpdateHighScoreText();
    }

    void HandleScoreChange()
    {
        coinCount++;
        coinText.IncrementCoinCount(coinCount);
        CheckHighScore();
    }

    void CheckHighScore()
    {
        if (coinCount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", coinCount);
            //Dynamically update HighScore
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = $"{PlayerPrefs.GetInt("HighScore", 0)}";
    }
}