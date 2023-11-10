using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Base : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    
    //For tracking the amount of health an object has vvv
    [HideInInspector]
    public int Health_Current;
    [HideInInspector]
    public int Health_Total;


    public void SetHealth(int health)
    {
        //Default slider value is 0-100.
        slider.value = health;
        Health_Current = health;
        fill.color = gradient.Evaluate(3f);
    }

    public void GainHealth(int health)
    {
        if (Health_Current < Health_Total)
        {
            Health_Current += health;
            slider.value = Health_Current;
            fill.color = gradient.Evaluate(3f);
            if(Health_Current > Health_Total)
            {
                Health_Current = Health_Total;
                slider.value = Health_Current;
                fill.color = gradient.Evaluate(3f);
            }
        }
        else
        {          
            print("Health cannot exceed total amount! Health_Current = " + Health_Current);
        }
    }

    public void SetMaxHealth(int health)
    {
        //Sets max health and value will change from 0 - health parameter.
        slider.maxValue = health;
        slider.value = health;
        Health_Current = health;
        Health_Total = health;

        fill.color = gradient.Evaluate(3f);
    }

    public virtual void TakeDamage(int damage)
    {
        Health_Current -= damage;
        slider.value = Health_Current;
        //to change color for health bar v
        fill.color = gradient.Evaluate(slider.normalizedValue);
        Dead();
    }
    public virtual void Dead()
    {
        // Vary outcome of dead:
        // Player: Change scene
        // Enemy: destroys it self ( Possibly drop coins ).
        if (slider.value <= 0)
        {
            print("Object doesn't have a dead feature ):");
        }
    }

    void Start()
    {
        //Defualt health 
        //You can change within Enemy and player health script and use SetMaxHealth() or SetHealth()
        SetMaxHealth(100);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
