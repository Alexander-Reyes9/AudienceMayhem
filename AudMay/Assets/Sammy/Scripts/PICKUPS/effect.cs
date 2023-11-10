using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class effect : MonoBehaviour
{
    public string effectType;
    public float duration;
    public GameObject canvas;
    public effectController ec;
    public bool trigger = false;


    //Scripts
    public Countdown countdown;
   

    void OnTriggerEnter(Collider other)
    {
        //Setting scripts
        ec = GameObject.FindGameObjectWithTag("Player").GetComponent<effectController>();

        

        if (other.tag.Equals("Player"))
        {
            ApplyEffect();
            gameObject.SetActive(false);
            print("effect applied");
        }
        
    }

    public void ApplyEffect()
    {
        GameObject other = Instantiate(canvas.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        countdown = canvas.GetComponent<Countdown>();
        Debug.Log("spawn canvas");
        countdown.timeStart = duration;
        countdown.effect = effectType;
        countdown.Run();
        Debug.Log("Does this script have an effect controller reference : " + ec);
        ec.effect = effectType;
        ec.trigger = true;
        Debug.Log(ec.trigger);


    }

    



    


    
}
