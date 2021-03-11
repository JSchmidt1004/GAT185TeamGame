using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float staminaMax;
    public float restore;
    public bool destroyOnDeath = false;

    public GameObject destroySpawnObject;
    public Slider slider;
    public UnityEvent deathEvent;

    public float stamina { get; set; }
    public bool isDead { get; set; } = false;
    
    void Start()
    {
        ResetHealth();
        slider = GetComponentInParent<ConnectSlider>().staminaSlider;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Game.Instance.State == Game.eState.Game)
        {
            AddStamina(Time.deltaTime * restore);
            if(slider != null)
            {
                slider.value = stamina / staminaMax;
            }
        }
    }

    public void AddStamina(float amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, staminaMax);
    }

    public void ResetHealth()
    {
        stamina = staminaMax;
    }
}
