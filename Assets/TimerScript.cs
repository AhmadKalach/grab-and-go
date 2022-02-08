using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Time: " + PlayerPrefs.GetFloat("Time").ToString("F1") + "s";
    }
}
