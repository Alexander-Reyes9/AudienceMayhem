using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies :MonoBehaviour
{

    public int numberOfEnemies;
    public int maxEnemies; //max number of enemies in one room
    public GameObject enemy; //enemy to spawn
    public float spawnInterval;

    public float spawnHeight;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    public AutoMove am;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This script is running!");
        am = GameObject.FindGameObjectWithTag("turtle").GetComponent<AutoMove>();
        //sets the bounds of where the enemies can spawn on the plane
        if (gameObject.name.Equals("Floor3") || gameObject.name.Equals("Floor4"))
        {
            minX = transform.position.x - (transform.localScale.x);
            maxX = transform.position.x + (transform.localScale.x);
            minZ = transform.position.z - (transform.localScale.z);
            maxZ = transform.position.z + (transform.localScale.z);
        }
        else if (gameObject.name.Equals("Floor1"))
        {
            minX = transform.position.x - (60 * transform.localScale.x);
            maxX = transform.position.x + (60 * transform.localScale.x);
            minZ = transform.position.z - (60 * transform.localScale.z);
            maxZ = transform.position.z + (60 * transform.localScale.z);
        }
        else
        {
            minX = transform.position.x - (2 * transform.localScale.x);
            maxX = transform.position.x + (2 * transform.localScale.x);
            minZ = transform.position.z - (2 * transform.localScale.z);
            maxZ = transform.position.z + (2 * transform.localScale.z);
        }

        //calls Spawn function after spawnInterval and continues
        InvokeRepeating("Spawn", spawnInterval, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //stops spawning enemies once maxEnemies is reached
        if(numberOfEnemies == maxEnemies)
        {
            CancelInvoke();
        }
    }

    public void Spawn()
    {
        RandomMovement script = enemy.GetComponent<RandomMovement>();
        if(script != null)
        {
            enemy.GetComponent<RandomMovement>().minX = minX;
            enemy.GetComponent<RandomMovement>().maxX = maxX;
            enemy.GetComponent<RandomMovement>().minZ = minZ;
            enemy.GetComponent<RandomMovement>().maxZ = maxZ;
        }
        //Set this back to spawnHeight
        GameObject other = Instantiate(enemy, new Vector3(Random.Range(minX, maxX), 7, Random.Range(minZ, maxZ)), Quaternion.identity);
        am.createdObjects.Add(other);
        numberOfEnemies++;
    }

}
