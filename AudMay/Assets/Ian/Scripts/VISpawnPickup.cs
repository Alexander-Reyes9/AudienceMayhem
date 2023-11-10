using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class VISpawnPickup : NetworkBehaviour // Changed inheritance from MonoBehaviour to NetworkBehaviour
{

    Vector3 position;
    float nEdge;
    float sEdge;
    float wEdge;
    float eEdge;
    public GameObject[] pickups;
    public int numOfObj = 0;
    public AutoMove am;

    //Ian's changes
    HistoryOfItems historyOfItems;

    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        historyOfItems = GameObject.FindGameObjectWithTag("Player").GetComponent<HistoryOfItems>();
        Debug.Log(historyOfItems);
        
        am = GameObject.FindGameObjectWithTag("turtle").GetComponent<AutoMove>();

        SetVars();

        Spawn(numOfObj);
    }

    void SetVars()
    {
        position = transform.position;
        nEdge = position.z + transform.localScale.z / 2;
        sEdge = position.z - transform.localScale.z / 2;
        wEdge = position.x - transform.localScale.x / 2;
        eEdge = position.x + transform.localScale.x / 2;
    }

    void Spawn(int num)
    {
        for(int i = 0;i <= num; i++)
        {
            float randX = Random.Range(wEdge, eEdge);
            float randZ = Random.Range(sEdge, nEdge);

            int rand = Random.Range(0, pickups.Length);

            GameObject other = Instantiate(pickups[rand], new Vector3(randX, transform.position.y + 1f, randZ), transform.rotation);
            am.createdObjects.Add(other);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }




    //Ian's Script Changes

    public void SpawnSpecificPickup(string pickup)
    {
        float randX = Random.Range(wEdge, eEdge);
        float randZ = Random.Range(sEdge, nEdge);

        foreach (GameObject go in items)
        {
            if(pickup.Trim().ToLower() == go.name.Trim().ToLower())
            {
                Debug.Log(go.name);
                Logger.Log(go.name);
                GameObject prefab = Instantiate(go, new Vector3(randX, transform.position.y, randZ), transform.rotation) as GameObject;
                Debug.Log(prefab.name + " has been instantiated");
                Logger.Log(prefab.name + " has been instantiated");
                Debug.Log(prefab);
                historyOfItems.AddNewGameObject(prefab);
                
            }
        }

        Debug.Log("Pickup could not be found");
    }

    
}

