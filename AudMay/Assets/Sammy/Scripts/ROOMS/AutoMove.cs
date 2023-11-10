using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public int facing = 0;  // -1 = facing west     0 = facing north    1 = facing east
    public int prevFacing = 0;
    public float spacing = 10.0f;
    public Vector3 position;
    public Quaternion rotation;
    public float xPos = 0.0f;
    public float zPos = 0.0f;
    public float yRot = 0.0f;
    public int rand;
    public GameObject baseSpawner;
    public GameObject straight;
    public GameObject left;
    public GameObject right;
    public GameObject shop;
    public int shopPer = 10;

    public ArrayList createdObjects = new ArrayList();

    public GameObject spawnArea;

    public int spawnDoor;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject other = Instantiate(baseSpawner, transform.position, transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Control()
    {

        position = transform.position;
        rotation = transform.rotation;
        xPos = position.x;
        zPos = position.z;
        yRot = rotation.y;

        SpawnArea();
        //SpawnRoom();
        if (facing == -1)    //either go straight if last one is -1 or go left is last one is 0
        {
            rand = Random.Range(-1, 1);
            SpawnRoom();
            Move(rand);
        } else if (facing == 0)
        {
            rand = Random.Range(-1, 2); // either 
            SpawnRoom();
            Move(rand);

        }
        else
        {
            rand = Random.Range(0, 2);
            SpawnRoom();
            Move(rand);

        }

        prevFacing = facing;
        facing = rand;


        //SpawnRoom();
        //GameObject other = Instantiate(baseSpawner, transform.position, transform.rotation);
    }

    public void SpawnRoom()
    {
        if (facing == rand)
        {

            //go straight

            int spawnRand = Random.Range(0, 100);
            if(spawnRand > shopPer)
            {
                GameObject other = Instantiate(straight, transform.position, transform.rotation);
                createdObjects.Add(other);
            }
            else
            {
                GameObject other = Instantiate(shop, transform.position, transform.rotation);
                createdObjects.Add(other);
            }
            
        }
        else
        {
            if (facing > rand)
            {
                //turn left
                GameObject other = Instantiate(left, transform.position, transform.rotation);
                createdObjects.Add(other);
            }
            else
            {
                //turn right
                GameObject other = Instantiate(right, transform.position, transform.rotation);
                createdObjects.Add(other);
            }
        }

    }

    public void Move(int dir)
    {
        if (dir == -1)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            xPos -= spacing;
        } else if (dir == 0)
        {
            //yRot = 0.0f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            zPos += spacing;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //yRot = 90.0f;
            xPos += spacing;
        }
        //rotation.y = yRot;
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        transform.position = new Vector3(xPos, 0, zPos);

    }

    void SpawnArea()
    {
        GameObject other = Instantiate(spawnArea, transform.position, transform.rotation);
        createdObjects.Add(other);
    }

    public void DestroyLevel()
    {
        foreach (GameObject j in createdObjects)
        {

            Destroy(j);
        }
    }

    public void ResetVar()
    {
        facing = 0;
        prevFacing = 0;
    }
    
}


