using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructRoom : MonoBehaviour
{
    public bool middle = false;
    public int wallPos;
    public string type;
    // top left = 0 top middle = 1  top right = 2
    public bool doorway = false;
    public RoomTemplete templetes;
    public AutoMove am;
    public int rand;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        templetes = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplete>();
        am = GameObject.Find("Turtle").GetComponent<AutoMove>();
        if (type.Equals("wall"))
        {
            SpawnWall();
        } else if (type.Equals("floor"))
        {
            SpawnFloor();
        } else if (type.Equals("ceiling"))
        {
            SpawnCeiling();
        }

    }

    void SpawnCeiling()
    {
        rand = Random.Range(0, templetes.ceilings.Length);
        obj = Instantiate(templetes.ceilings[rand], transform.position, transform.rotation);
        am.createdObjects.Add(obj);
    }

    void SpawnFloor()
    {
        rand = Random.Range(0, templetes.floors.Length);
        obj = Instantiate(templetes.floors[rand], transform.position, transform.rotation);
        am.createdObjects.Add(obj);
    }

    void SpawnWall()
    {

        if (!doorway)
        {
            rand = Random.Range(0, templetes.walls.Length);
            obj = Instantiate(templetes.walls[rand], transform.position, transform.rotation);
            am.createdObjects.Add(obj);
        }
        else
        {
            rand = Random.Range(0, templetes.doorways.Length);
            obj = Instantiate(templetes.doorways[rand], transform.position, transform.rotation);
            am.createdObjects.Add(obj);
        }
    }

    void SetDoorway(bool d)
    {
        doorway = d;
    }

    
}
