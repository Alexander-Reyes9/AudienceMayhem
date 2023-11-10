using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInSight : attack
{
    public GameObject weapon;
    private CapsuleCollider wCC;
    attack attackScript;
    private bool enemyInSight;
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        wCC = weapon.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //raycast of camera
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //eventually: if raycat hits something
        //and attacking, and within a 5 foot radius then
/*
        if ((Physics.Raycast(ray, out hit)) && (isAttacking) && (IfInRadFPS(hit.transform.position)))
        {
            //if rayycast hit == enemy
            //then check if 
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                print("enemy is being attacked");
                Destroy(hit.collider.gameObject);
            } else {
                print("an enemy is not being attacked, something else is");
			}
            
            //if hit enemy then check if collision between enemy with weapon
            //hit.
        }
        else
        {
            print("attack is not possible");
        }
        */
    }

    //checks the distance between the player and the enemy
    private bool IfInRadFPS(Vector3 d)
    {
        float dist = Vector3.Distance(d, transform.position);

        //if something is in sight the check if it is an enemy 
        //if so, enemy is in sight, else false
        if (dist < 3.0F)
        {
            enemyInSight = true;
        }
        else
        {
            enemyInSight = false;
        }
        return enemyInSight;
    }
    
}
