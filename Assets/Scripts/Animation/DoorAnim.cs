using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorAnim : MonoBehaviour
{
    [Range(1, 10)]public float speed = 1.0f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Open");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Close");
        }
    }
}
