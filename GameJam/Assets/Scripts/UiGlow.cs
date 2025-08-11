using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UiGlow : MonoBehaviour
{
    public Image image;
    private MainScript ms;
    private float opacity = 0.5f;
    private bool aAdd = true;
    private float mod = 5;
    private bool isRecovering = false;
    private float recoverySpeed = 1f;

    private void Start()
    {
        ms = FindAnyObjectByType<MainScript>();

        // Start with green glow
        float initialHpFraction = 1f;
        image.color = new Color(0, Mathf.Clamp(1 - initialHpFraction, 0, 0.5f) * 2, 0, opacity);

        EventManager.AddListener<PlayerHitEvent>(FlashRedAndRecover);
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

        if (isRecovering)
        {
            // Smoothly transition back to color based on hpFraction
            Color targetColor = new Color(
                Mathf.Clamp(hpFraction, 0, 0.5f) * 2,
                Mathf.Clamp(1 - hpFraction, 0, 0.5f) * 2,
                0,
                opacity
            );
            image.color = Color.Lerp(image.color, targetColor, recoverySpeed * Time.deltaTime);

            if (Mathf.Abs(image.color.g - targetColor.g) < 0.01f)
            {
                isRecovering = false;
            }
        }
        else
        {
            // Update image color based on the hpFraction
            image.color = new Color(Mathf.Clamp(hpFraction, 0, 0.5f) * 2, Mathf.Clamp(1 - hpFraction, 0, 0.5f) * 2, 0, opacity);
        }
    }

    // Function to briefly change color to red and then recover
    public void FlashRedAndRecover(PlayerHitEvent evt)
    {
        image.color = new Color(1, 0, 0, opacity); // Set to red
        isRecovering = true; // Start the recovery process in Update
    }
}
