using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public bool moving = true;
    private float speed = 7;
    public AudioSource hitSound;

    void Start()
    {
        if (moving)
        {
            speed = Random.Range(5, 16); 
        }
        else
        {
            speed = 0;
        }
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (transform.position.z >= 60 || transform.position.z <= -60)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Add score to game
        if (collision.gameObject.CompareTag("Projectile"))
        { 
            hitSound.Play();  
        }
    }
}
