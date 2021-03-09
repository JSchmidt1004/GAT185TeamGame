using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amount = 10;
    public GameObject spawnObject;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(amount);
            if (spawnObject != null)
            {
                //Possibly parent with other.transform, need check though
                GameObject gameObject = Instantiate(spawnObject, transform.position, transform.rotation);
                Destroy(gameObject, 2);
            }

            Destroy(gameObject);
        }
    }
}
