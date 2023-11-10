using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

//Ian's Changes
using UnityEngine.SceneManagement;
public class CreateRoomScript : MonoBehaviour
{
    //be sure to make host room in player
    //Room that the player hosts
    private Unity.Services.Lobbies.Models.Lobby hostsRoom;
    //A boolean variable used to start a coruntine for heartbeating a host's room
    private bool heartbeatFlag;
    //copy of the ID of a room of a host
    private string roomIDString;
    private RelayHostData relayHostData = new RelayHostData();
    //remember relay integrations....



    //Ian's Changes
    [SerializeField]
    private HostClientState stateTracker;
    // Start is called before the first frame update
    void Start()
    {

    }



    public async void CreateRoomAsync(string s, int maxConnections)
    {

        relayHostData = await RelayManager.SetupRelay();

        //initializes options of the room.
        CreateLobbyOptions options = new CreateLobbyOptions();

        options.IsPrivate = false;
        options.Data =
            new Dictionary<string, DataObject>()
            {
                {
                    "joinCode",
                    new DataObject(visibility: DataObject
                            .VisibilityOptions
                            .Public,
                        value: relayHostData.JoinCode,
                        index: DataObject.IndexOptions.S1)
                }
            };
        options.Player =
            new Player(id: AuthenticationService.Instance.PlayerId,
                // This is the "player scoped" data for the host player
                data: new Dictionary<string, PlayerDataObject>()
                {
                    {
                        "ExampleMemberPlayerData",
                        new PlayerDataObject(visibility: PlayerDataObject
                                .VisibilityOptions
                                .Member,
                            value: "ExampleMemberPlayerData")
                    }
                });

        hostsRoom = await Lobbies.Instance.CreateLobbyAsync(s, maxConnections, options);
        roomIDString = hostsRoom.Id;
        //listOfLobbies.Add(testLobby);
        heartbeatFlag = true;

        //Starts Host when creating lobby
        NetworkManager.Singleton.StartHost();

        //Ian's Changes
        stateTracker.hostRoomId = hostsRoom.Id;
        stateTracker.IsHost = true;
        Debug.Log("Is this player a host: " + NetworkManager.Singleton.IsHost);
        //End

        //setting up hosts of lobbies.
        GameObject myPlayerObj = GameObject.Find("Player");
        NetworkObject networkObj = myPlayerObj.GetComponent<NetworkObject>();
        if (networkObj == null)
        {
            Debug.Log("There is no network object component attached");
        }
        else
        {
            Debug.Log("There IS a network object attached to the gameobject");
        }
        MyPlayer myPlayerObjScript = myPlayerObj.GetComponent<MyPlayer>();
        myPlayerObjScript.IncrementRoomCountServerRpc();

        //Ian's Changes
        NetworkManager.Singleton.SceneManager.LoadScene("AudFinal", LoadSceneMode.Single);
    }


    //This is used to heartbeat lobbies.
    IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        var delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (!roomIDString.Equals(""))
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
