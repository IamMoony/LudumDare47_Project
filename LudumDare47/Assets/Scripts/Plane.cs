using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioSource sound_Engine;
    public AudioSource sound_Explosion;
    public GameObject effect_Explosion;
    public Button button_Accelerate;
    public Button button_Brake;

    private UIManager uiManager;
    private float curPitch;
    private float curSpd;
    private float curThrust;
    private float curRot;

    void Awake()
    {
        curSpd = flyingSpeed;
        curThrust = thrust;
        curRot = rotationSpeed;
        curPitch = initialPitch;
        sound_Engine.pitch = curPitch;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x > .5f)
            {
                curThrust = thrust * thrustMultiplierGas;
                curSpd = flyingSpeed * speedMultiplierGas;
                curRot = rotationSpeed * rotationMultiplierGas;
            }
            else if (Input.GetTouch(0).position.x <= .5f)
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
        }
        else if (Input.touchCount == 0)
            {
            if (Input.GetButton("Fire1"))
            {
                curThrust = thrust * thrustMultiplierGas;
                curSpd = flyingSpeed * speedMultiplierGas;
                curRot = rotationSpeed * rotationMultiplierGas;
            }
            else if (Input.GetButton("Fire2"))
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
        }
       
        curPitch = Mathf.Lerp(curPitch, initialPitch + (rb.velocity.magnitude / flyingSpeed - 1) * 2, pitchAmountPerFrame * Time.deltaTime);
        sound_Engine.pitch = curPitch;
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
        {
            uiManager.GameOver();
            sound_Engine.Stop();
            Instantiate(effect_Explosion, transform.position, Quaternion.identity);
            Instantiate(sound_Explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
