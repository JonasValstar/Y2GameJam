using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
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
        rect.Translate(0, Screen.height/10 * Time.deltaTime, 0);
        aliveTime += Time.deltaTime;
        if (aliveTime >= 2) { 
            Destroy(gameObject); 
        }
    }
}