using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    private  GameObject player;
    private float speed = 10;
    public Rigidbody rb;
    private Vector3 rotationVector;
    private NavMeshAgent agent;
    private float dist;
    private Vector3 playerpos;
    //private Camera cam;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MalePlayer");
        //cam = player.GetComponent<Camera>();
        rb.velocity = new Vector3(speed, 0, speed);
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.Warp(transform.position);
        //agent.SetDestination(player.transform.position);
        //agent.destination = player.transform.position;
        //rb.constraints = RigidbodyConstraints.FreezeRotationY;
        //rotationVector = transform.rotation.eulerAngles;
        //rotationVector.y = 0;
        //rb.constraints = RigidbodyConstraints.FreezeRotationX;        
        //rb.constraints = RigidbodyConstraints.FreezeRotationY;
        //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        //transform.LookAt(player.transform);
        //transform.position += transform.forward * speed * Time.deltaTime;

        //playerpos.x = player.transform.position.x;
        //playerpos.y = 0;
        //playerpos.z = player.transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        playerpos = new Vector3(2, 2, player.transform.position.z);
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > 5.0F)
        {
            transform.LookAt(playerpos);
        }

    }
}
