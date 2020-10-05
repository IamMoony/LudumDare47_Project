using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    public float flyingSpeed;
    public Vector2 flyingDirection;

    void FixedUpdate()
    {
        transform.Translate(flyingDirection * flyingSpeed * Time.fixedDeltaTime);
    }
}
