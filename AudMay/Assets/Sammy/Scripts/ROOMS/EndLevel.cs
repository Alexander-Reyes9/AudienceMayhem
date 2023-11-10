using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public LevelController lc;
    public GameObject player;
    public AutoMove am;
    public GameObject turtle;
    public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        turtle = GameObject.Find("Turtle");
        am = GameObject.Find("Turtle").GetComponent<AutoMove>();
        lc = GameObject.Find("Level").GetComponent<LevelController>();
        player = GameObject.Find("FPSController");
        cc = player.GetComponent<CharacterController>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            lc.AdvLevel();
            am.DestroyLevel();
            turtle.transform.position = new Vector3(0.0f, 0.0f, 120.0f);
            turtle.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            am.ResetVar();
            lc.Run();
            cc.enabled = false;
            player.transform.position = new Vector3(0.0f, 10f, 0.0f);
            cc.enabled = true;
        }
    }
}
