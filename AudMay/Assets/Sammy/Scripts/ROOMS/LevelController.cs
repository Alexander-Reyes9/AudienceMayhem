using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int level = 1;
    public AutoMove turtleController;
    public int turns = 10;
    public GameObject endRoom;
    public GameObject startRoom;
    public GameObject player;
    
    // Start is called before the first frame update
    public void Start()
    {

        Run();

    }

    public void AdvLevel()
    {
        level++;
    }

    public void Run()
    {
        turtleController = GameObject.Find("Turtle").GetComponent<AutoMove>();
        GameObject other = Instantiate(startRoom, transform.position, transform.rotation);
        turtleController.createdObjects.Add(other);
        turns += level;
        for (int i = 0; i < turns; i++)
        {
            turtleController.Control();
        }
        other = Instantiate(endRoom, turtleController.transform.position, turtleController.transform.rotation);
        print("Spawned End Room");
        turtleController.createdObjects.Add(other);
       // player.GetComponent<HistoryOfItems>().Start();
    }
}
