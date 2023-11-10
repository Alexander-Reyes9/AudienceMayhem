using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private int health;

    public GameObject item1; //item to drop when enemy dies
    //public GameObject item2;
    public int amount1; //amount of items (coins/hearts) instantiated at death
    //public int amount2;
    private int enemyHealth; //field to check health/death of enemy

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //gets health variable from enemy
        //health = GetComponent<Health_Enemy>.Health_Current;

        //calls DropItems method when enemy dies / when health is 0
        /*if(health == 0)
        {
            DropItems();
            AddToHistory(item1);
            AddToHistory(item2);
        }*/

        //calls DropItems method 
    }

    //replacing enemy death (0 health) to test script
    /*
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            gameObject.SetActive(false);

            DropItems();

            AddToHistory();
        }
    }
    */
    void DropItems()
    {
        for (int i = 0; i < amount1; i++)
        {
            Instantiate(item1, transform.position, Quaternion.identity);
        }

        /*for (int i = 0; i < amount2; i++)
        {
            Instantiate(item2, transform.position, Quaternion.identity);
        }*/
    }

    public void AddToHistory()
    {
        for (int i = 0; i < amount1; i++)
        {
            player.GetComponent<HistoryOfItems>().AddNewGameObject(item1);
        }

        /*for (int i = 0; i < amount2; i++)
        {
            player.GetComponent<HistoryOfItems>().AddNewGameObject(item2);
        }*/
    }
}
