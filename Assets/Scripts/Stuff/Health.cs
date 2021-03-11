using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public bool destroyOnDeath = false;

    public GameObject destroySpawnObject;
    public Slider slider;
    public UnityEvent deathEvent;

    public float health { get; set; }
    public bool isDead { get; set; } = false;
    
    void Start()
    {
        ResetHealth();
        slider = GetComponentInParent<ConnectSlider>().healthSlider;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Game.Instance.State == Game.eState.Game)
        {
            AddHealth(-Time.deltaTime * decay);
            if(slider != null)
            {
                slider.value = health / healthMax;
            }

            if (health <= 0 && !isDead)
            {
                isDead = true;
                deathEvent?.Invoke();
                if (destroySpawnObject != null)
                {
                    Instantiate(destroySpawnObject, transform.position, transform.rotation);
                }
                if (destroyOnDeath) GameObject.Destroy(gameObject);
            }
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }

    public void ResetHealth()
    {
        health = healthMax;
    }
}
