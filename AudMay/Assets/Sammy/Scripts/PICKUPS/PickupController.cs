using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public GameObject[] pickups;
    public int index = -1;
    public AutoMove am;

    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("Turtle").GetComponent<AutoMove>();
        if (index == -1)
        {
            index = Random.Range(0, pickups.Length);
            Spawn();
        }
        else
        {
            Spawn();
        }
    }

    void SetIndex(int i)
    {
        i = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject other = Instantiate(pickups[index], transform.position, transform.rotation);
        am.createdObjects.Add(other);
    }
}
