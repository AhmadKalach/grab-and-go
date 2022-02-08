using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedrunTime : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = "Time: " + (Time.unscaledTime - startTime).ToString("F1") + "s";
    }
}
