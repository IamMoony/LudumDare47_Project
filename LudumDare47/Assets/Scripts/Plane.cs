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

    public Rigidbody2D rb;
    public AudioSource audioS;

    private float curSpd;
    private float curThrust;
    private float curRot;

    void Start()
    {
        curSpd = flyingSpeed;
        curThrust = thrust;
        curRot = rotationSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            curThrust = thrust * 2f;
            //curSpd = flyingSpeed * 1.5f;
            curRot = rotationSpeed * .5f;
        }
        else if (Input.GetMouseButton(1))
        {
            curThrust = thrust * .5f;
            //curSpd = flyingSpeed * .75f;
            curRot = rotationSpeed * 2f;
        }
        else
        {
            curThrust = thrust;
            //curSpd = flyingSpeed;
            curRot = rotationSpeed;
        }
        audioS.pitch = 0.5f + rb.velocity.magnitude / curSpd;
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
