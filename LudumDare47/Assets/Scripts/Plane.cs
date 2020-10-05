using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plane : MonoBehaviour
{
    public float rotationSpeed;
    public float flyingSpeed;
    public float thrust;
    public float liftForce;
    public float thrustMultiplierGas;
    public float thrustMultiplierBrake;
    public float speedMultiplierGas;
    public float speedMultiplierBrake;
    public float rotationMultiplierGas;
    public float rotationMultiplierBrake;
    public float initialPitch;
    public float pitchAmountPerFrame;

    public Rigidbody2D rb;
    public AudioSource audioS;

    private float curPitch;
    private float curSpd;
    private float curThrust;
    private float curRot;

    void Start()
    {
        curSpd = flyingSpeed;
        curThrust = thrust;
        curRot = rotationSpeed;
        curPitch = initialPitch;
        audioS.pitch = curPitch;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            curThrust = thrust * thrustMultiplierGas;
            curSpd = flyingSpeed * speedMultiplierGas;
            curRot = rotationSpeed * rotationMultiplierGas;
        }
        else if (Input.GetMouseButton(1))
        {
            curThrust = thrust * thrustMultiplierBrake;
            curSpd = flyingSpeed * speedMultiplierBrake;
            curRot = rotationSpeed * rotationMultiplierBrake;
        }
        else
        {
            curThrust = thrust;
            curSpd = flyingSpeed;
            curRot = rotationSpeed;
        }
        curPitch = Mathf.Lerp(curPitch, initialPitch + (rb.velocity.magnitude / flyingSpeed - 1) * 2, pitchAmountPerFrame * Time.deltaTime);
        audioS.pitch = curPitch;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * curThrust);
        rb.rotation += curRot * rb.velocity.magnitude / curSpd;
        Vector2 relforce = Vector2.up * liftForce * Vector2.Dot(rb.velocity, Vector2.down);
        rb.AddForce(rb.GetRelativeVector(relforce));
        if (rb.velocity.magnitude > curSpd)
            rb.velocity = rb.velocity.normalized * curSpd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
