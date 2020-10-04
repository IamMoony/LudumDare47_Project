using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float flyingSpeed;
    public float liftForce;
    public float liftDuration;
    public float liftDelay;
    public float liftDelayVariance;

    private float curDelay;
    private float curDuration;
    private Rigidbody2D rb;
    private AudioSource audioS;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();
        curDelay = liftDelay + Random.Range(-liftDelayVariance, liftDelayVariance);
        curDuration = liftDuration;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * flyingSpeed, rb.velocity.y);
        if (curDelay > 0)
            curDelay -= Time.fixedDeltaTime;
        else
        {
            if (curDuration > 0)
            {
                rb.AddForce(Vector2.up * liftForce * Time.fixedDeltaTime);
                curDuration -= Time.fixedDeltaTime;
            }
            else
            {
                curDuration = liftDuration;
                curDelay = liftDelay + Random.Range(-liftDelayVariance, liftDelayVariance);
            }
        }
    }
}
