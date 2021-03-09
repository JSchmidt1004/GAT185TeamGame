﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnRate = 180.0f;

    Weapon weapon;
    Rigidbody rb;
    ParticleSystem ps;
    Vector2 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
        weapon = GetComponent<Weapon>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(Vector3.up * turnRate * inputDirection.x);
        rb.AddRelativeForce(Vector3.forward * speed * inputDirection.y);

        var main = ps.main;
        main.startLifetime = inputDirection.y;
    }

    public void OnMove(InputValue input)
    {
        inputDirection = input.Get<Vector2>();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnThrow()
    {
        weapon.Fire(transform.forward);
    }
}
