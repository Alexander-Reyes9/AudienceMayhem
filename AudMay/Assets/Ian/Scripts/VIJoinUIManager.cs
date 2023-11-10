using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VIJoinUIManager : MonoBehaviour
{
    //buttons
    private GameObject joinRoomButton;
    private GameObject quickJoinButton;
    private GameObject inputInstructions;
    private GameObject lobbyIDInput;
    private GameObject refreshButton;
    private GameObject textingInputField;
    private GameObject messageHistory;
    private bool IsJoin;
    private string roomCode;
    //Empty object used to refer to network joining methods
    private GameObject joinLobbyManager;
    private GameObject textList;
    // Start is called before the first frame update
    void Start()
    {
        joinRoomButton = GameObject.Find("JoinRoomButton");
        quickJoinButton = GameObject.Find("QuickJoinButton");
        inputInstructions = GameObject.Find("InputInstructions");
        lobbyIDInput = GameObject.Find("LobbyIDInput");
        joinRoomButton.SetActive(false);
        IsJoin = false;
        joinLobbyManager = GameObject.Find("JoinLobbyManager");
        GameObject player = GameObject.Find("Player");
        textList = GameObject.Find("LobbyList");
        refreshButton = GameObject.Find("RefreshButton");
        textingInputField = GameObject.Find("TextingInputField");
        messageHistory = GameObject.Find("MessageHistory");
        //remove commenting for lines 39 & 40 after testing messaging
        textingInputField.SetActive(false);
        messageHistory.SetActive(false);

        //Ian's changes
        //player.GetComponent<MyPlayer>().StartClient();
    }

    public void JoinRoomButtonPressed()
    {
        JoinButtonPressed();
        joinLobbyManager.GetComponent<VIJoinRoomScript>().JoinRoom(roomCode);
    }

    public void QuickJoinButtonPressed()
    {
        JoinButtonPressed();
        joinLobbyManager.GetComponent<VIJoinRoomScript>().QuickJoin();
    }

    public void BackButtonPressed()
    {
        if (!IsJoin)
            Debug.Log("Call to return back to home screen");
        else
        {
            Debug.Log("Call to reset Join Scene");
            IsJoin = false;
        }
    }

    public void ReadStringInput(string zx)
    {
        roomCode = zx;
        Debug.Log("Inputted room code: "+ roomCode);
        joinRoomButton.SetActive(true);
    }

    public void RefreshButtonPressed()
    {
        string list = JoinRoomScript.printRoomList();
        //set this call equal to text of a textObject;
        textList.GetComponent<TextMeshProUGUI>().text = list;
    }

    void JoinButtonPressed()
    {
        joinRoomButton.SetActive(false);
        quickJoinButton.SetActive(false);
        IsJoin = true;
        inputInstructions.SetActive(false);
        lobbyIDInput.SetActive(false);
        refreshButton.SetActive(false);
        textList.SetActive(false);
        textingInputField.SetActive(true);
        messageHistory.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
