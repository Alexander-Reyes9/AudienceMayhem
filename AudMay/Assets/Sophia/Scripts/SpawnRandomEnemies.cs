using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemies : MonoBehaviour
{
    private int numberOfEnemies; 
    public int maxEnemies; //max number of enemies in one room
    public float spawnInterval;

    public float spawnHeight;

    public GameObject enemyRandom;
    public GameObject enemyPlayer;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
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
        if (numberOfEnemies == maxEnemies)
        {
            CancelInvoke();
        }
    }

    //decides to spawn enemy that follows the player or enemy with random movement
    void Spawn()
    {
        int x = (int)Random.Range(0.0f, 2.0f);

        if (x == 0)
        {
            SpawnRandomMov();
        }
        else
        {
            SpawnPlayerMov();
        }
    }

    //spawns enemy with random movement
    void SpawnRandomMov()
    {
        Debug.Log(enemyRandom.GetComponent<RandomMovement>());
        enemyRandom.GetComponent<RandomMovement>().minX = minX;
        enemyRandom.GetComponent<RandomMovement>().maxX = maxX;
        enemyRandom.GetComponent<RandomMovement>().minZ = minZ;
        enemyRandom.GetComponent<RandomMovement>().maxZ = maxZ;

        Instantiate(enemyRandom, new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ)), Quaternion.identity);
        numberOfEnemies++;
    }

    //spawns enemy that follows the player
    void SpawnPlayerMov()
    {
        Instantiate(enemyPlayer, new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ)), Quaternion.identity);
        numberOfEnemies++;
    }
}
