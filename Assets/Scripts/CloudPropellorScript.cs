using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPropellorScript : MonoBehaviour
{
    public float upDistance;
    public float upwardsVelocity;
    public float downwardsVelocity;

    bool shooting;
    bool goingUp;
    float initialY;
    float targetY;
    float lastShotTime;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialY = transform.localPosition.y;
        targetY = initialY + upDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shooting = true;
                goingUp = true;
            }
        }

        if (shooting)
        {
            if (goingUp)
            {
                rb.velocity = new Vector2(rb.velocity.x, upwardsVelocity);
                if (transform.localPosition.y > targetY)
                {
                    goingUp = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -downwardsVelocity);
                if (transform.localPosition.y < initialY)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    shooting = false;
                }
            }
        }
    }
}
