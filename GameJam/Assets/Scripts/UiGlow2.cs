using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGlow2 : MonoBehaviour
{
    public Image image;
    float opacity = 0.5f;
    bool aAdd = true;
    float mod = 5;

    // Update is called once per frame
    void Update()
    {
        // updating opacity
        if (aAdd) {
            if (opacity >= 0.6) { aAdd = false; mod = Random.Range(4f, 6f); }
            opacity += Time.deltaTime / mod;
        } else {
            if (opacity <= 0.3) { aAdd = true; mod = Random.Range(4f, 6f); }
            opacity -= Time.deltaTime / mod;
        }

        image.color = new Color(.7f, .7f, .7f, opacity);
    }
}
