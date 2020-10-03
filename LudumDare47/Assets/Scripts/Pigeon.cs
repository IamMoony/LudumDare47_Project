using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    public float flyingSpeed;

    void FixedUpdate()
    {
        transform.Translate(Vector2.left * flyingSpeed * Time.fixedDeltaTime);
    }
}
