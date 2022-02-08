using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunButton : MonoBehaviour
{
    public GameObject speedrunButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Won", 0) == 1)
        {
            speedrunButton.SetActive(true);
        }
        else
        {
            speedrunButton.SetActive(false);
        }
    }
}
