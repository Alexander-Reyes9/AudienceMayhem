using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timeStart = 60;
    public TextMeshProUGUI textBox;
    public effectController ec;
    //public vsc_FirstPersonController fpc;

    public string effect;    
    // Start is called before the first frame update
    void Start()
    {
        //Scripts 
        //fpc = FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        ec = GameObject.FindGameObjectWithTag("Player").GetComponent<effectController>();


    }

    public void Run()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        timeStart -= Time.deltaTime;
        if (timeStart < 0)
        {
            ec.StopEffect();
            Destroy(gameObject);
        }
        textBox.text = Mathf.Round(timeStart).ToString();

        
        
        if (timeStart > 0)
        {
            if (effect.Equals("jump"))
            {

                Debug.Log("jump");
            } 
        }
        


    }
}
