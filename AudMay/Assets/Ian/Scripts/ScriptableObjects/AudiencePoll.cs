using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudiencePoll", menuName = "AudiencePoll")]
public class AudiencePoll : ScriptableObject
{
    public string question;
    public Choice choice1;
    public Choice choice2;
    public Choice choice3;
    public Choice choice4;


}
