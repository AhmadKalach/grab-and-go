using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceToShootScript : MonoBehaviour
{
    public float shootDistance;
    public float shootTime;
    public float retractTime;

    Rigidbody2D rb;
    bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !shooting)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shooting = true;
        Sequence sequence = DOTween.Sequence();
        Vector3 startPos = transform.position;

        sequence.Append(rb.DOMove(startPos + (transform.up * shootDistance), shootTime));
        sequence.Append(rb.DOMove(startPos, retractTime));
        sequence.AppendCallback(() => shooting = false);

        sequence.Play();
    }
}
