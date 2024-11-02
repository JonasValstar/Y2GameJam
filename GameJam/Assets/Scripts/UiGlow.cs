using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UiGlow : MonoBehaviour
{
    public Image image;
    private MainScript ms;
    float opacity = 0.5f;
    bool aAdd = true;
    float mod = 5;

    private void Start()
    {
        ms = FindObjectOfType<MainScript>();
    }

    void Update()
    {
        // Updating opacity as before
        if (aAdd)
        {
            if (opacity >= 0.6f) { aAdd = false; mod = Random.Range(4f, 6f); }
            opacity += Time.deltaTime / mod;
        }
        else
        {
            if (opacity <= 0.3f) { aAdd = true; mod = Random.Range(4f, 6f); }
            opacity -= Time.deltaTime / mod;
        }

        // Access the game timer's elapsed time
        float hpFraction = ms.GetGameTimerElapsedTime();
        //Debug.Log(hpFraction);

        // Update image color based on the hpFraction
        image.color = new Color(Mathf.Clamp(hpFraction, 0, 0.5f) * 2, Mathf.Clamp(1 - hpFraction, 0, 0.5f) * 2, 0, opacity);

    }
}
