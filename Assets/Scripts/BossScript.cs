using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossScript : MonoBehaviour
{
    public bool playerEnter;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public SpriteRenderer bodySprite;
    public SpriteRenderer faceSprite;


    [Space]
    [Header("Movement")]
    public float leftX;
    public float rightX;
    public float XVelocity;
    public bool goingLeft;

    [Space]
    [Header("Combat")]
    public AudioSource hitSound;
    public float shakeTime;
    public float shakeStrength;
    public int health = 3;
    public float invincibilityTime = 0.5f;
    public float waitBeforeEnd = 4f;
    public ParticleSystem deathParticles;

    bool isDead;
    float lastHitTime;
    Rigidbody2D rb;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        deathParticles.Stop();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (playerEnter && !isDead)
        {
            if (goingLeft)
            {
                rb.velocity = new Vector2(-XVelocity, 0);
                if (transform.localPosition.x < leftX)
                {
                    rb.velocity = new Vector2(XVelocity, rb.velocity.y);
                    goingLeft = !goingLeft;
                }
            }
            else
            {
                rb.velocity = new Vector2(XVelocity, 0);
                if (transform.localPosition.x > rightX)
                {
                    rb.velocity = new Vector2(-XVelocity, rb.velocity.y);
                    goingLeft = !goingLeft;
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            if (playerController == null)
            {
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            }
            playerEnter = playerController.isBossFight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Time.time > lastHitTime + invincibilityTime)
            {
                hitSound.Play();
                Shake();
                health--;
                lastHitTime = Time.time;
                if (health == 2)
                {
                    leftLeg.transform.parent = null;
                    leftLeg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    leftLeg.GetComponent<Rigidbody2D>().velocity = rb.velocity;

                    rightLeg.transform.parent = null;
                    rightLeg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    rightLeg.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                }
                else if (health == 1)
                {
                    leftHand.transform.parent = null;
                    leftHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    leftHand.GetComponent<Rigidbody2D>().velocity = rb.velocity;

                    rightHand.transform.parent = null;
                    rightHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    rightHand.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                }
                else if (health == 0)
                {
                    StartCoroutine(Die(waitBeforeEnd));
                }
            }
        }
    }

    void Shake()
    {
        transform.DOShakePosition(shakeTime, shakeStrength);
    }

    private IEnumerator Die(float waitTime)
    {
        deathParticles.Play();
        isDead = true;
        bodySprite.sprite = null;
        faceSprite.sprite = null;
        yield return new WaitForSeconds(waitTime);
        playerController.won = true;
        Destroy(this.gameObject);
    }
}
