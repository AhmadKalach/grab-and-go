using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShooterScript : MonoBehaviour
{
    public float shootDistance;
    public float shootTime;
    public float retractTime;
    public float timeBetweenShots;
    public float lastShotTime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 startPos = transform.position;

        sequence.Append(rb.DOMove(startPos + (transform.up * shootDistance), shootTime));
        sequence.Append(rb.DOMove(startPos, retractTime));

        sequence.Play();
    }
}
