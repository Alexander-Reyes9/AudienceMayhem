using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject arm;
    Animator anim;
    public static bool isAttacking;
    private int log;
    private static float cooldown = 0.00f;
    // Start is called before the first frame update
    void Start()
    {
        anim = arm.GetComponent<Animator>();

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            attackMode();
        }
        Debug.Log("isAttacking: " + isAttacking);
    }

    public void attackMode()
    {
        if (cooldown < Time.time)
        {
            cooldown = Time.time + 2f;
            anim.SetInteger("state", 1);
            isAttacking = true;
        }
    }
    public void stopAttacking()
    {
        anim.SetInteger("state", 0);
        isAttacking = false;
    }
}