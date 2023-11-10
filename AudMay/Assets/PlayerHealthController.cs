using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    Health_Player HealthController;

    void Start()
    {
        HealthController = GameObject.Find("PlayerHealthCanvas").GetComponent<Health_Player>();
        print("HealthController = " + HealthController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthController.TakeDamage(5);
        }
    }
}

