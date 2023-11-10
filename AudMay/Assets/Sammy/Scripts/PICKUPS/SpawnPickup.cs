using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
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
    effectController ec;

    // Start is called before the first frame update
    void Start()
    {
        //Ian's Changes
        historyOfItems = GameObject.FindGameObjectWithTag("Player").GetComponent<HistoryOfItems>();
        
        am = GameObject.FindGameObjectWithTag("turtle").GetComponent<AutoMove>();

        ec = GameObject.FindGameObjectWithTag("Player").GetComponent<effectController>();

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
        for (int i = 0; i <= num; i++)
        {
            float randX = Random.Range(wEdge, eEdge);
            float randZ = Random.Range(sEdge, nEdge);

            int rand = Random.Range(0, pickups.Length);

            GameObject other = Instantiate(pickups[rand], new Vector3(randX, transform.position.y+5f, randZ), transform.rotation);
            other.AddComponent<Light>();
            other.GetComponent<Light>().color = Color.white;
            //Quaternion rotation = new Quaternion();
            //other.GetComponent<Light>().transform.localScale = new Vector3(.25f,.25f,.25f);
            other.GetComponent<Light>().transform.rotation = new Quaternion(-90, 0, 0, 0); ;
            other.GetComponent<Light>().spotAngle = 45;
            other.GetComponent<Light>().intensity = 8;
            other.GetComponent<Light>().type = LightType.Point;
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
            if (pickup.Trim().ToLower() == go.name.Trim().ToLower())
            {
                Debug.Log(go.name);
                Logger.Log(go.name);
                GameObject prefab = Instantiate(go, new Vector3(randX, transform.position.y, randZ), transform.rotation) as GameObject;
                Debug.Log(prefab.name + " has been instantiated");
                Logger.Log(prefab.name + " has been instantiated");
                Debug.Log(prefab);

                if(prefab.tag.ToLower() == "effect")
                {
                    prefab.GetComponent<effect>().ec = ec;
                    prefab.GetComponent<effect>().ApplyEffect();
                }

                historyOfItems.AddNewGameObject(prefab);
                prefab.SetActive(false);
                return;
            }
        }

        Debug.Log("Pickup could not be found");
    }
}
