using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClose : MonoBehaviour
{
    private Text text;
    private float timer = 0;
    
    private void Awake()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    private void Update()
    {
        if (text.enabled)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                text.enabled = false;
                timer = 0;
            }
        }
    }
}
