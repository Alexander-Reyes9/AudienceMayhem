using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Choice : ScriptableObject
{
    public string text;

    public bool canChangeRooms;

    public bool canSpawnObject;

    public string objectToSpawn;

    public int voteCount = 0;
}
