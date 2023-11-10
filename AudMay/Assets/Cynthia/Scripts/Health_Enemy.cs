using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Enemy : Health_Base
{
    
    //IMPORTANT: Once in devlop drag this prefab under "Enemy" prefab that shophia has
    
    //player cam
    public Camera playerCam;
    //health canvas
    public CanvasGroup EnemyUI;
    //public EnemyHealthController parentHealth;
    
    void Start()
    {
        SetMaxHealth(100);
        playerCam = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        print("Current health is " + Health_Current);
        EnemyUI = GetComponent<CanvasGroup>();
        //EnemyUI.alpha = 0;


    }

    public override void TakeDamage(int damage)
    {
        //makes UI visible
        if(EnemyUI.alpha == 0)
        {
            EnemyUI.alpha = 0.6f;
        }
        

        Health_Current = Health_Current - damage;
        slider.value = Health_Current;
        //to change color for health bar v
        fill.color = gradient.Evaluate(slider.normalizedValue);
       // print("Health is now " + Health_Current);
        Dead();
    }

    public override void Dead()
    {
        if (slider.value <= 0)
        {
            //Object destroys itself if dead
            //Add code for dropping coins (random amount)
            print("Enemy is now dead");
            //Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
       //used for UI to look at the player
        transform.LookAt(transform.position + playerCam.transform.position);
    }

    void Update()
    {
        /*
        SetHealth(parentHealth.health);
       // print(parentHealth.health);
        if(parentHealth.health == null)
        {
            print("This is null!");
        }
        */

    }
}
