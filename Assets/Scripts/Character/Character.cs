﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public enum eSpace
    {
        World,
        Camera,
        Object
    }

    public enum eMovement
    {
        Free,
        Tank,
        Strafe
    }

    [Range(0, 20)] public float speed = 1.0f;
    [Range(0, 20)] public float jump = 1.0f;
    [Range(-20, 20)] public float gravity = -9.8f;

    public Animator animator;
    public Weapon weapon;
    public eSpace space = eSpace.World;
    public eMovement movement = eMovement.Free;
    public float turnRate = 3;

    bool onGround = false;
    CharacterController characterController;
    Vector3 inputDirection;
    Vector3 velocity;
    Transform cameraTransform;

    public Health health { get; set; }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (animator.GetBool("Death")) return;

        {
            onGround = characterController.isGrounded;
            if (onGround && velocity.y < 0)
            {
                velocity.y = 0;
            }

            // ***
            Quaternion orientation = Quaternion.identity;
            switch (space)
            {
                case eSpace.Camera:
                    Vector3 forward = cameraTransform.forward;
                    forward.y = 0;
                    orientation = Quaternion.LookRotation(forward);
                    break;
                case eSpace.Object:
                    orientation = transform.rotation;
                    break;
                default:
                    break;
            }

            Vector3 direction = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            switch (movement)
            {
                case eMovement.Free:
                    direction = orientation * inputDirection;
                    rotation = (direction.sqrMagnitude > 0) ? Quaternion.LookRotation(direction) : transform.rotation;
                    break;
                case eMovement.Tank:
                    direction.z = inputDirection.z;
                    direction = orientation * direction;

                    rotation = orientation * Quaternion.AngleAxis(inputDirection.x, Vector3.up);
                    break;
                case eMovement.Strafe:
                    direction = orientation * inputDirection;
                    rotation = Quaternion.LookRotation(orientation * Vector3.forward);
                    break;
                default:
                    break;
            }
            // ***

            characterController.Move(direction * speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnRate * Time.deltaTime);


            //Animator
            animator.SetFloat("Speed", inputDirection.magnitude);
            animator.SetBool("OnGround", onGround);
            animator.SetFloat("VelocityY", velocity.y);

            //Gravity Movement
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        if (health.health <= 0)
        {
            animator.SetBool("Death", true);
        }
    }

    public void OnDeath()
    {
        animator.SetBool("Death", true);
        EventManager.Instance.TriggerEvent("PlayerDead");
    }

    public void ResetCharacter()
    {
        health.ResetHealth();
        animator.SetTrigger("Live");
    }

    public void OnFire()
    {
        weapon.Fire(transform.forward);
    }

    public void OnJump()
    {
        if (onGround)
        {
            velocity.y += jump;
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 v2 = input.Get<Vector2>();

        inputDirection.x = v2.x;
        inputDirection.z = v2.y;
    }

    public void OnPunch()
    {
        animator.SetTrigger("Punch");
    }

    public void OnThrow()
    {
        //if (Game.Instance.State == Game.eState.Game)
        {
            animator.SetTrigger("Throw");
        }
    }

}
