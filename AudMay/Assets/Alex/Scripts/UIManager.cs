using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Netcode;
//Integrate NetworkManager code into UIScript.
//place on empty game object, UI Manager.
public class UIManager : MonoBehaviour
{
    //These are the GameObjects that are set active and false throughout the navigation of joining and creating a lobby
    public static GameObject LobbyInfoInputter;

    private GameObject MakeLobby;

    private GameObject JoinLobby;

    private GameObject BackButton;

    private GameObject ListOfLobbies;

    private string debugPanel;
    // Start is called before the first frame update
    void Start()
    {
        
        LobbyInfoInputter = GameObject.FindWithTag("LobbyInfoInputter");
        MakeLobby = GameObject.Find("CreateroomButton");
        JoinLobby = GameObject.Find("JoinLobby");
        BackButton = GameObject.Find("BackButton");
        ListOfLobbies = GameObject.Find("ListOfLobbies");
        ListOfLobbies.SetActive(true);
        LobbyInfoInputter.SetActive(false);
        BackButton.SetActive(false);


        NetworkManager.Singleton.OnClientConnectedCallback += ((id) =>
        {
            Debug.Log("Player has connected!");
            Logger.Log("Player has connected!");
        });

        NetworkManager.Singleton.OnClientDisconnectCallback += ((id) =>
        {
            Debug.Log("Player has disconnected!");
            Logger.Log("Player has disconnected!");
        });
    }


    private void OnGUI()
    {
        debugPanel = GUI.TextField(new Rect(100, 0, Screen.width * 1/4, Screen.height * 1/4), Logger.document);
    }


    // Update is called once per frame
    public void OnMakeClick()
    {
        JoinLobby.SetActive(false);
        MakeLobby.SetActive(false);
        BackButton.SetActive(true);
        LobbyInfoInputter.SetActive(true);
        //for final set to false, true for testing purposes
        ListOfLobbies.SetActive(false);
    }

    public void OnBackClick()
    {
        JoinLobby.SetActive(true);
        MakeLobby.SetActive(true);
        BackButton.SetActive(false);
        LobbyInfoInputter.SetActive(false);
        ListOfLobbies.SetActive(true);
    }
    public void ClickJoinButton()
    {
        MakeLobby.SetActive(false);
        BackButton.SetActive(true);
        LobbyInfoInputter.SetActive(false);
        //setUpClient
        NetworkManager.Singleton.StartClient();
        
    }
    public void GenerateListLobbies ()
    {
        ListOfLobbies.GetComponent<TextMeshProUGUI>().text = LobbiesScript.getRoomList();
    }

    
    

}
