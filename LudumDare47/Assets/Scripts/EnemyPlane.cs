using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPlane : MonoBehaviour
{
    public float rotationSpeed;
    public float flyingSpeed;
    public float thrust;
    public float liftForce;

    private Rigidbody2D rb;
    private AudioSource audioS;

    private float curSpd;
    private float curThrust;
    private float curRot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();
        curSpd = flyingSpeed;
        curThrust = thrust;
        curRot = rotationSpeed;
    }

    void Update()
    {
        audioS.pitch = 0.5f + rb.velocity.magnitude / curSpd;
    }

    private void FixedUpdate()
    {
        rb.AddForce(-transform.right * curThrust);
        rb.rotation -= curRot * rb.velocity.magnitude / curSpd;
        Vector2 relforce = Vector2.up * liftForce * Vector2.Dot(rb.velocity, Vector2.down);
        rb.AddForce(rb.GetRelativeVector(relforce));
        if (rb.velocity.magnitude > curSpd)
            rb.velocity = rb.velocity.normalized * curSpd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
