using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class Health_Player : Health_Base
{
    //This script is for the player asset.

    void Start()
    {
        SetMaxHealth(100);
    }
    
    public override void Dead()
    {
        if(slider.value <= 0)
        {
            //Will change to dead scene (add code)
            NetworkManager.Singleton.SceneManager.LoadScene("Ded", LoadSceneMode.Single);
            print("Player is now dead");
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            GainHealth(20);
        }
    }

 
}
