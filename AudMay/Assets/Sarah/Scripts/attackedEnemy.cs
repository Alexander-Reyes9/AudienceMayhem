using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackedEnemy : attack
{
    // Start is called before the first frame update
    // public EnemyHealthController HealthController;
    public Health_Enemy HealthController;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("collision detected, " + transform.position);
        if (isAttacking)
        {
            if (other.CompareTag("Enemy"))
            {
                if (HealthController.Health_Current <= 0)
                {
                    Destroy(other.gameObject);
                }
                HealthController = other.gameObject.transform.Find("EnemyHealthCanvas").GetComponent<Health_Enemy>();
                HealthController.TakeDamage(30);
                print("enemy hit");
                Debug.Log("Activated");
            }
        }
        

    }
}
