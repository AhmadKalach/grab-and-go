using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameObject spikes;
    public float spikesScaleTime;
    public List<Collision2D> currCollisions;
    public float fallMultiplier;
    public float jointBreakForce;
    public float jointBreakTorque;
    public FixedJoint2D currJoint;
    public bool wantToStick;
    public bool sticked;
    public bool stickedAfterWantingToStick;
    public bool isBossFight;
    public bool won;
    public SceneChanger sceneChanger;

    [Space]
    [Header("Collisions")]
    public LayerMask groundLayer;
    public float collisionRadius;

    [Space]
    [Header("Sounds")]
    public AudioSource rollingAudioSource;
    public float rollingStepPerVelocity;
    public float minBounceVelocity;
    public AudioSource bouncingAudioSource;
    public float bouncingStepPerVelocity;
    public AudioSource levelMusic;
    public AudioSource bossMusic;

    float startTime;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.unscaledTime;
        currCollisions = new List<Collision2D>();
        rb = GetComponent<Rigidbody2D>();
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleStickiness();
        HandleSounds();
        BetterGravity();

        if (won)
        {
            PlayerPrefs.SetFloat("Time", Time.unscaledTime - startTime);
            PlayerPrefs.SetInt("Won", 1);
            sceneChanger.GoToEndGameCinematic();
        }
    }

    void HandleSounds()
    {
        bool isOverlapping = Physics2D.OverlapCircle(transform.position, collisionRadius, groundLayer);
        if (isOverlapping)
        {
            if (!rollingAudioSource.isPlaying)
            {
                rollingAudioSource.Play();
            }
            rollingAudioSource.volume = Mathf.Clamp(Mathf.Abs(rb.angularVelocity) * rollingStepPerVelocity, 0, 1);
        }
        else
        {
            rollingAudioSource.Stop();
        }
    }

    void RemoveSpikes()
    {
        if (spikes.transform.localScale.x >= 1)
        {
            spikes.transform.DOScale(0, spikesScaleTime);
        }
    }

    void PutSpikes()
    {
        if (spikes.transform.localScale.x <= 0)
        {
            spikes.transform.DOScale(1, spikesScaleTime);
        }
    }

    void HandleStickiness()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantToStick = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            wantToStick = false;
            stickedAfterWantingToStick = false;
            Unstick();
        }

        if (!wantToStick)
        {
            Unstick();
            RemoveSpikes();
        }

        if (wantToStick)
        {
            PutSpikes();
        }

        if (wantToStick && sticked)
        {
            stickedAfterWantingToStick = true;
        }

        if (gameObject.GetComponents<FixedJoint2D>().Length < 1)
        {
            sticked = false;
        }

        if (stickedAfterWantingToStick && !sticked)
        {
            wantToStick = false;
        }

        if (currCollisions.Count > 0)
        {
            if (wantToStick)
            {
                StickToObject(currCollisions[currCollisions.Count - 1]);
                sticked = true;
            }
        }
    }

    void StickToObject(Collision2D col)
    {
        if (gameObject.GetComponents<FixedJoint2D>().Length < 1)
        {
            currJoint = gameObject.AddComponent<FixedJoint2D>();
            currJoint.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
            currJoint.connectedAnchor = transform.position - col.gameObject.transform.position;
            currJoint.breakForce = jointBreakForce;
            currJoint.breakTorque = jointBreakTorque;
            currJoint.autoConfigureConnectedAnchor = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }
    }

    void Unstick()
    {
        sticked = false;
        currJoint = null;
        FixedJoint2D[] joints = gameObject.GetComponents<FixedJoint2D>();
        foreach (FixedJoint2D joint in joints)
        {
            Destroy(joint);
        }
    }

    void BetterGravity()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("BossRoom"))
        {
            GetComponent<Animator>().SetTrigger("BossFight");
            isBossFight = true;

            if (levelMusic.isPlaying)
            {
                levelMusic.Pause();
                bossMusic.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (rb.velocity.magnitude > minBounceVelocity)
        {
            if (!col.gameObject.tag.Equals("Unstickable"))
            {
                bouncingAudioSource.volume = rb.velocity.magnitude * bouncingStepPerVelocity;
                bouncingAudioSource.Play();
            }
        }

        if (!col.gameObject.tag.Equals("Unstickable"))
        {
            currCollisions.Add(col);
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.tag.Equals("Unstickable"))
        {
            currCollisions.Remove(col);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, collisionRadius);
    }
}
