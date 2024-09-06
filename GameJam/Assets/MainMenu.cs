using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text heigh;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Last Score: " + PlayerPrefs.GetInt("LastScore");
        heigh.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
