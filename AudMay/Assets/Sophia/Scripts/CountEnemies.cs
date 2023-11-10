using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for Sammy: keeps track of the number of enemies in the scene
public class CountEnemies : MonoBehaviour
{
    private int numberOfEnemies;
    private GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;
    }

    //to test if numberOfEnemies is correct
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            //Debug.Log(numberOfEnemies);
        }
    }
}
