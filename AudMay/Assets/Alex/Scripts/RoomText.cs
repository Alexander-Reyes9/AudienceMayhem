using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RoomText : MonoBehaviour
{
    private string lobbyName;

    //max number of players in a single lobby, set to 100 which is the max.
    private static int numOfPlayers = 10;

    private GameObject LobbyManagerObj;
    private InputField lobbyInputter;
    public LobbiesScript lobbiesScript;
    
    void Start()
    {
        LobbyManagerObj = GameObject.Find("LobbyManager");
        lobbyInputter = GameObject.Find("LobbyInfoInputter").GetComponent<InputField>();
        lobbiesScript = new LobbiesScript();
    }
    //fix this method to be able to get text from the text field, figure out the issue.
    
   public void JoinButton()
   {
       UIManager UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
       UIManager.ClickJoinButton();
   } 
    
    
    
    public void ReadStringInput(string s)
    {
        lobbyName = s;
        Debug.Log("This is " + lobbyName);
        //textInfo = GameObject.FindWithTag("TextFromLobbyInfo").GetComponent<TextMeshPro>();
        lobbiesScript.CreateRoomAsync (lobbyName, numOfPlayers);
        UIManager.LobbyInfoInputter.SetActive(false);
        //figure out how to clear text
    }
}
