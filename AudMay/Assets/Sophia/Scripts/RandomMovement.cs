using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NewPosition", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = newPosition;
    }

    void NewPosition()
    {
        //0.5f->5.5f
        newPosition = new Vector3(Random.Range(minX, maxX),14f, Random.Range(minZ, maxZ));
    }

}
