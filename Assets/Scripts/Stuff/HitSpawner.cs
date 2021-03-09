using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpawner : MonoBehaviour
{
    public GameObject spawnGameObject;
    public float lifetime = 5.0f;
    public bool useLifeTime = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameObject gameObject = Instantiate(spawnGameObject, transform);
            if (useLifeTime)
            {
                Destroy(gameObject, lifetime);
            }
        }
    }
}
