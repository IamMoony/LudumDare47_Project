using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float flyingSpeed;
    public float flyingSpeedVariance;
    public float liftForce;
    public float liftForceVariance;
    public float liftDuration;
    public float liftDurationVariance;
    public float liftDelay;
    public float liftDelayVariance;

    private float curSpeed;
    private float curForce;
    private float curDelay;
    private float curDuration;
    private Rigidbody2D rb;
    private AudioSource audioS;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();
        curDelay = liftDelay + Random.Range(-liftDelayVariance, liftDelayVariance);
        curDuration = liftDuration + Random.Range(-liftDurationVariance, liftDurationVariance);
        curSpeed = flyingSpeed + Random.Range(-flyingSpeedVariance, flyingSpeedVariance);
        curForce = liftForce + Random.Range(-liftForceVariance, liftForceVariance);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * curSpeed, rb.velocity.y);
        if (curDelay > 0)
            curDelay -= Time.fixedDeltaTime;
        else
        {
            if (curDuration > 0)
            {
                rb.AddForce(Vector2.up * curForce * Time.fixedDeltaTime);
                curDuration -= Time.fixedDeltaTime;
            }
            else
            {
                curDuration = liftDuration + Random.Range(-liftDurationVariance, liftDurationVariance);
                curDelay = liftDelay + Random.Range(-liftDelayVariance, liftDelayVariance);
            }
        }
    }
}
