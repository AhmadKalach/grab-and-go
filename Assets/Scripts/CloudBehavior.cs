using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    public float leftX;
    public float rightX;
    public float XVelocity;
    public bool goingLeft;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (goingLeft)
        {
            transform.localPosition = new Vector3(rightX, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(leftX, transform.localPosition.y, transform.localPosition.z);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (goingLeft)
        {
            rb.velocity = new Vector2(-XVelocity, rb.velocity.y);
            if (transform.localPosition.x < leftX)
            {
                rb.velocity = new Vector2(XVelocity, rb.velocity.y);
                goingLeft = !goingLeft;
            }
        }
        else
        {
            rb.velocity = new Vector2(XVelocity, rb.velocity.y);
            if (transform.localPosition.x > rightX)
            {
                rb.velocity = new Vector2(-XVelocity, rb.velocity.y);
                goingLeft = !goingLeft;
            }
        }
    }
}
