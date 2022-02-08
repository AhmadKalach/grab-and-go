using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematicScript : MonoBehaviour
{
    public GameObject image2;
    public SceneChanger sceneChanger;

    int spaceCounter;

    // Start is called before the first frame update
    void Start()
    {
        spaceCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCounter++;
            if (spaceCounter == 1)
            {
                image2.SetActive(true);
            }
            else if (spaceCounter == 2)
            {
                sceneChanger.GoToGame();
            }
        }
    }
}
