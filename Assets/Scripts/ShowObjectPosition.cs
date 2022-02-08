using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjectPosition : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "X: " + gameObject.transform.position.x + " Y: " + gameObject.transform.position.y;
    }
}
