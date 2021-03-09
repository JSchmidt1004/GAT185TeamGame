using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour
{
    [Range(0, 20)] public float speed = 1.0f;
    [Range(0, 20)] public float jump = 1.0f;

    Vector3 inputDirection;
    Vector3 velocity;
    
    void Update()
    {
        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        velocity = inputDirection * speed;
        transform.Translate(velocity * Time.deltaTime);
        //transform.position += velocity * Time.deltaTime;
    }
}
