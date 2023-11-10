using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public string type; //weapon    effect  potion
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }

    void CheckType()
    {
        if (!(type.Equals(null)))
        {
            if (type.Equals("weapon"))
            {
                //make weapon
            } else if (type.Equals("effect"))
            {
                //make effect
            }
            else
            {
                //make money
            }
        }
    }

    void SetType(string s)
    {
        type = s;
    }

    void SetName(string s)
    {
        name = s;
    }
}
