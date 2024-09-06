using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UiGlow : MonoBehaviour
{
    public Image image;
    [SerializeField] MainScript ms;
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

        float hpFraction = (float)ms.time / (float)ms.maxTime;
        Debug.Log(hpFraction);
        image.color = new Color(Mathf.Clamp( 1 - hpFraction, 0, .5f)*2, Mathf.Clamp(hpFraction, 0, .5f)*2, 0, opacity);
    }
}
