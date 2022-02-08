using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunUI : MonoBehaviour
{
    public GameObject timer;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Speedrun", 0) == 0)
        {
            timer.SetActive(false);
            restartButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Speedrun", 0) == 1)
        {
            timer.SetActive(true);
            restartButton.SetActive(true);
        }
    }
}
