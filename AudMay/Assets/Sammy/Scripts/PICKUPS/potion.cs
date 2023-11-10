using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public string effectType;   //  strength    infinite jump   blindness   
    public int index = -1;
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        if (index != -1)
        {
            setEffect();
        }
        else
        {
            int rand = Random.Range(0, prefabs.Length);
            index = rand;
            setEffect();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }

    void setEffect()
    {


        for(int i = 0; i < prefabs.Length; i++)
        {
            if(i == index)
            {
                GameObject other = Instantiate(prefabs[i], transform.position, transform.rotation);
            }
        }

    }
}
