using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiencePollLibrary : MonoBehaviour
{

    public List<AudiencePoll> libraryOfPolls;

    public AudiencePoll initialQuestion;
    public AudiencePoll secondQuestion;
    public AudiencePoll thirdQuestion;
    public AudiencePoll fourthQuestion;
    public AudiencePoll fifthQuestion;
    public AudiencePoll sixthQuestion;
    public AudiencePoll seventhQuestion;
    public AudiencePoll eighthQuestion;
    
    void Start()
    {
        libraryOfPolls = new List<AudiencePoll>();
        CreateQuestionSets();

    }

    public void CreateQuestionSets()
    {
        initialQuestion = CreateQuestion("What weapon should the player have?", CreateChoice("Sword", false, true, "swordspikeslow"), CreateChoice("Hammer", false, true, "hammer"), 
        CreateChoice("Axe", false, true, "axe"), CreateChoice("Bow", false, false, "bow"));

        /*secondQuestion = CreateQuestion("The monsters are now...", CreateChoice("huge", false, false, null), CreateChoice("tiny", false, false, null), CreateChoice("", false, false, null), CreateChoice("", false, false, null));

        thirdQuestion = CreateQuestion("Oh no... the monsters are now..", CreateChoice("faster", false, false, null), CreateChoice("stronger", false, false, null), 
        CreateChoice("healthier", false, false, null), CreateChoice("", false, false, null));

        fourthQuestion = CreateQuestion("Let's help the player out!", CreateChoice("faster", false, false, null), CreateChoice("stronger", false, false, null),
        CreateChoice("healthier", false, false, null), CreateChoice("", false, false, null));

        fifthQuestion = CreateQuestion("Oh no... the player is now...", CreateChoice("slower", false, false, null), CreateChoice("weaker", false, false, null), CreateChoice("sick", false, false, null), 
        CreateChoice("", false, false, null));

        sixthQuestion = CreateQuestion("Rooms are now...", CreateChoice("Icy", true, false, null), CreateChoice("Sticky", true, false, null), CreateChoice("Monstrous", true, false, null), CreateChoice("", false, false, null));
        */
        seventhQuestion = CreateQuestion("Care Package incoming!", CreateChoice("Heart", false, true, "heart"), CreateChoice("Bomb", false, true, "bomb"),
        CreateChoice("Monster", false, true, "monster"), CreateChoice("Coin", false, true, "money"));

        eighthQuestion = CreateQuestion("Pick an item!", CreateChoice("Pogostick", false, true, "pogostick"), CreateChoice("Cupcake", false, true, "cupcake"), CreateChoice("AncientBook", false, true, "ancientbook"),
            CreateChoice("Eyepatch", false, true, "eyepatch"));
        
    }


    private Choice CreateChoice(string text, bool canChangeRooms, bool canSpawnObject, string objectToSpawn){
        Choice newChoice = (Choice)ScriptableObject.CreateInstance(typeof(Choice));
        newChoice.text = text;
        newChoice.canChangeRooms = canChangeRooms;
        newChoice.canSpawnObject = canSpawnObject;
        newChoice.objectToSpawn = objectToSpawn;

        return newChoice;

    }
    private AudiencePoll CreateQuestion(string question, Choice choice1, Choice choice2, Choice choice3, Choice choice4){

        AudiencePoll newQuestion = (AudiencePoll)ScriptableObject.CreateInstance(typeof(AudiencePoll));    

        //Configure new AudiencePoll
        newQuestion.question = question;
        newQuestion.choice1 = choice1;
        newQuestion.choice2 = choice2;
        newQuestion.choice3 = choice3;
        newQuestion.choice4 = choice4;

        //Add new AudiencePoll to list
        libraryOfPolls.Add(newQuestion);
        return newQuestion;
    }
}
