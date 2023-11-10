using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class CreateUIManager : MonoBehaviour
{
    //Declaring Gameobjects
    private GameObject backButton;
    private GameObject createRoomButton;
    private GameObject roomIDInput;
    //Empty gameobject used to refer to network create room method
    private GameObject createLobbyManager;
    //holds the name the host named their room, be sent to CreateRoomScript
    private string roomName;
    private bool isRoomMethodCalled;
    private GameObject NetworkManagerObj;
    // Start is called before the first frame update
    void Start()
    {
        backButton = GameObject.Find("backButton");
        createRoomButton = GameObject.Find("CreateroomButton");
        roomIDInput = GameObject.Find("RoomIDInput");
        createRoomButton.SetActive(false);
        isRoomMethodCalled = false;
        createLobbyManager = GameObject.Find("CreateLobbyManager");
        NetworkManagerObj = GameObject.Find("NetworkManager");
    }
    public void ReadStringInput(string l)
    {
        Debug.Log("String roomName is : " + l);
        roomName = l;
        createRoomButton.SetActive(true);
    }

    public void OnClickCreateRoom()
    {
        //Make an actual call to createRoom.
        createLobbyManager.GetComponent<CreateRoomScript>().CreateRoomAsync(roomName, 8);
        //Debug.Log("CreateRoom(\""+ roomName + "\")");
        createRoomButton.SetActive(false);
        roomIDInput.SetActive(false);
        backButton.SetActive(false);
        isRoomMethodCalled = true;
       //fix second parameter.
        // NetworkSceneManager.LoadScene("AudFinal", LoadSceneMode.Single);
    }

    public void OnClickBack()
    {
        //write a call to go back to menu from before.
        if (!isRoomMethodCalled)
            SceneManager.LoadScene("DecisionScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
