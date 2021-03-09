using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public EnemyShipData enemyShipData;

    Rigidbody rb;
    GameObject targetGameObject;
    Weapon weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
        targetGameObject = GameObject.FindGameObjectWithTag(enemyShipData.targetTag);
    }

    void FixedUpdate()
    {
        Vector3 direction = targetGameObject.transform.position - transform.position;
        Vector3 cross = Vector3.Cross(transform.forward, direction.normalized);

        float angle = (Vector3.Dot(direction, transform.forward) > 0) ? cross.y : Mathf.Sign(cross.y);
        rb.AddTorque(Vector3.up * angle * enemyShipData.turnRate);

        //rb.AddTorque(Vector3.up * cross.y * enemyShipData.turnRate);
        rb.AddRelativeForce(Vector3.forward * enemyShipData.speed);

        if (direction.magnitude < enemyShipData.fireRange)
        {
            weapon.Fire(transform.forward);
        }
    }
}
