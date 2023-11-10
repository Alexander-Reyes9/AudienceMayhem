using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int floorMask;

    public float speed = 0;
    private Rigidbody rb;
    public Transform cam;
    
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = cam.TransformDirection(movement);
        movement.y = 0f;
        rb.AddForce(movement * speed);
        Debug.DrawRay(transform.position, movement * speed);
    }

}

