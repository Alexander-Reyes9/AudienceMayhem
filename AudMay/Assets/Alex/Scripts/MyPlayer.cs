using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.UI;
//Integrate server, host, and rpc methods into player(s) scripts
public class MyPlayer : NetworkBehaviour
{
    //The maximum number of messages that could appear in the message history
    //Messages that are not the last maxMessages, will be deleted.
    public static int maxMessages = 5;
    public GameObject chatPanel; 
    public GameObject messageObject;
    private GameObject textingInputField;
    private static string userName; 
    [SerializeField]
    List<Message> messages = new List<Message>(); 
    // Start is called before the first frame update
    void Start()
    {
        GameObject NetworkManager = GameObject.Find("NetworkManager");
        GameObject player = GameObject.Find("Player");
        //fix if statement if I have time
        //if (player.GetComponent<NetworkObject>().IsLocalPlayer || player.GetComponent<NetworkObject>().IsOwner)
            //player.GetComponent<NetworkObject>().Spawn();
        textingInputField = GameObject.Find("TextingInputField");
        userName = "TEsst Name";
        //messageHistory = GameObject.Find("MessageHistory");
    }
    
    public void StartServer()
    {
        NetworkManager.GetComponent<NetworkManager>().StartServer();
    }
    
    public void StartClient()
    {
        NetworkManager.GetComponent<NetworkManager>().StartClient();
        Debug.Log("Client activated!");
    }

    public void endClient()
    {
        //NetworkManager.GetComponent<NetworkManager>().StopHost();
    }
    public void StartHost()
    {
        NetworkManager.GetComponent<NetworkManager>().StartHost();
    }
    //Method to be called by a computer that is a client and acted upon the server.
    [ServerRpc(RequireOwnership = false)]
    public void IncrementRoomCountServerRpc()
    {
        IncrementRoomCountClientRpc();
    }

    
    [ServerRpc(RequireOwnership = false)]
    public void IncrementRoomCountServerRpc(int num)
    {
        IncremementRoomCountClientRpc(num);
    }

    [ClientRpc]
    public void IncremementRoomCountClientRpc(int n)
    {
        LobbiesScript.roomCount.Value+= n;
        Debug.Log("Current rooms open in game: " + LobbiesScript.roomCount.Value);
        Logger.Log("Current rooms open in game: " + LobbiesScript.roomCount.Value);
    }


    //call from the server that changes something on all clients.
    [ClientRpc]
    public void IncrementRoomCountClientRpc()
    {
        LobbiesScript.roomCount.Value++;
        Debug.Log("Current rooms open in game: " +LobbiesScript.roomCount.Value);
        Logger.Log("Current rooms open in game: " + LobbiesScript.roomCount.Value);
    }
    //make sure you transform this to a server rpc-client rpc.
    public void SendMessageToChat (string m)
    {
        if (messages.Count >= maxMessages)
        {
            Destroy(messages[0].textObject.gameObject);
            messages.Remove(messages[0]);
        }

        Message newMessage = new Message();
        newMessage.text = userName + ": " + m;
        GameObject newText = Instantiate(messageObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObject.text = newMessage.text;
        messages.Add(newMessage);
    }
    // Update is called once per frame
    void Update()
    {
        /*//Debug.Log(textingInputField.GetComponent<InputField>());
        if (Input.GetKeyDown(KeyCode.Return))   
        {
            SendMessageToChat("This is a test"/*textingInputField.GetComponent<InputField>().text*///);
         /*   textingInputField.GetComponent<InputField>().text = "";
            Debug.Log("Enter pressed!");
        }
    */
    }
}
[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObject;
}
