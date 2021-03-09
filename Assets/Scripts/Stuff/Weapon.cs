using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0.1f;
    private int ammoMax = 100;
    private float fireTimer = 0;

    public Projectile Projectile;
    public Transform emitTransform;

    void Start()
    {
        
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    Projectile bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
        //    bullet.Fire(ray.direction);
        //}
    }

    public bool Fire(Vector3 position, Vector3 direction)
    {
        if(fireTimer >= fireRate)
        {
            fireTimer = 0;
            Projectile projectile = Instantiate(this.Projectile, position, Quaternion.identity);
            projectile.Fire(direction);

            return true;
        }

        return false;
    }

    public bool Fire(Vector3 direction)
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0;
            Projectile projectile = Instantiate(this.Projectile, emitTransform.position, emitTransform.rotation);
            projectile.Fire(direction);

            return true;
        }

        return false;
    }
}
