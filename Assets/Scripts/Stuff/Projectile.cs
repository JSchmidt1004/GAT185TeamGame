using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile: MonoBehaviour
{
    [Range(0, 5)] public float lifespan = 2.0f;
    [Range(1, 100)] public float speed = 20.0f;
    public bool destroyOnHit = false;
    public GameObject destroyEffect;

    private void OnDestroy()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }

    public void Fire(Vector3 forward)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(forward * speed, ForceMode.VelocityChange);
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnHit && !collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
