using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    // variables
    RectTransform rect;
    float aliveTime;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.Translate(0, Screen.height / 10 * Time.deltaTime, 0);

        aliveTime += Time.deltaTime;
        if (aliveTime >= 2)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rect.localScale *= 0.99f;
    }
}