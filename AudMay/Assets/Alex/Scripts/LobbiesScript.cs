using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Collections;
using System;
using System.Threading.Tasks;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Services.Core.Environments;

public class LobbiesScript : NetworkBehaviour
{
    //a lobby
    private Unity.Services.Lobbies.Models.Lobby testLobby;
    
    //flag variable for whether to start corountine lobby
    private static bool heartbeatFlag;
    
    //A copy of the lobby's id as a string.
    private static string lobbyidString;
    
    //Network list of type lobby named listOfLobbies
    private static List<Unity.Services.Lobbies.Models.Lobby> listOfLobbies = new List<Unity.Services.Lobbies.Models.Lobby>();
    
    //number of lobbies active
    public static NetworkVariable<int> roomCount = new NetworkVariable<int>(0);
    
    
    public static Text lobbiesList;

    //Obtains reference to UnityTransport component, which should be connected to same gameObject as NetworkManager
    public static UnityTransport Transport => NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

    private RelayHostData relayHostData = new RelayHostData();

    private RelayJoinData relayJoinData = new RelayJoinData();



    //Initializes all necessary player necessities for networking with other players
    async void Start()
    {
        await UnityServices.InitializeAsync();

        //Check if user is signed in
        //Sign them in anonymously if they are not signed in
        try {

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

        }
        catch (Exception error) {
            
            Debug.Log("User failed to sign in");
            Debug.Log(error);
            AuthenticationService.Instance.ClearSessionToken();

        }
        
    }
    



    //method that makes players join any public lobby 
    public async void ReadStringInput(string s)
    {

        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(s);
        
        //Obtains relay join code
        string joinCode = lobby.Data["joinCode"].Value;
        Debug.Log(joinCode);

        relayJoinData = await RelayManager.JoinRelay(joinCode);


        GameObject a = GameObject.Find("JoinLobby");
        RoomText aRoomText = a.GetComponent<RoomText>();
        aRoomText.JoinButton();
    }

    //attach this method to a quick join button.
    public async void QuickJoin()
    {
        // Quick-join a random lobby with a maximum capacity of 10 or more players.
        QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();
        /*options.Filter = new List<QueryFilter>()
        {
            new QueryFilter(
                field: QueryFilter.FieldOptions.MaxPlayers,
                op: QueryFilter.OpOptions.GE,
                value: "10")
        };*/


        Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync(options);
        // Obtains relay join code
        string joinCode = lobby.Data["joinCode"].Value;
        Debug.Log(joinCode);

        relayJoinData = await RelayManager.JoinRelay(joinCode);
        
    }
    
    //for loop that sets text on text object for the list of lobbies by names
    //returns names of the room written by hosts at initialization 
    public static string getRoomList() 
    {
        string fullString = "";
        foreach (Unity.Services.Lobbies.Models.Lobby a in listOfLobbies)
        {
            fullString += a.Name +":\t" + a.Id+"\n";
        }
        Logger.Log("Number of rooms open: " + roomCount.Value.ToString() + " " + fullString);
        return "Number of rooms open: " + roomCount.Value.ToString() + " " + fullString;      
    }
    
    //method used to create a room
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
 
        testLobby = await Lobbies.Instance.CreateLobbyAsync(s, maxConnections, options);
        lobbyidString = testLobby.Id;
        listOfLobbies.Add(testLobby);
        heartbeatFlag = true;

        //Starts Host when creating lobby
        NetworkManager.Singleton.StartHost();

        //setting up hosts of lobbies.
        GameObject myPlayerObj = GameObject.Find("PlayerTestObj");
        NetworkObject networkObj = myPlayerObj.GetComponent<NetworkObject>();
        if(networkObj == null){
            Debug.Log("There is no network object component attached");
        }
        else{
            Debug.Log("There IS a network object attached to the gameobject");
        }
        MyPlayer myPlayerObjScript = myPlayerObj.GetComponent<MyPlayer>();
        myPlayerObjScript.IncrementRoomCountServerRpc();
    }

    
    //This is used to heartbeat lobbies.
    IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        var delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (!lobbyidString.Equals(""))
        {
            Lobbies.Instance.SendHeartbeatPingAsync (lobbyId);
            Debug.Log("print every 5 seconds! and hearbeat!!");
            Logger.Log("print every 5 seconds! and heartbeat!!");
            yield return delay;
        }
    }

    //Update method hearbeats a lobby of the player if they have a lobby
    void Update()
    {
        if (heartbeatFlag)
        {
            StartCoroutine(HeartbeatLobbyCoroutine(lobbyidString, 5.0f));
            heartbeatFlag = false;
        }
    }    
    
    public async void RemovePlayer()
    {
        if (NetworkManager.Singleton.IsClient && NetworkManager.Singleton.IsConnectedClient)
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            await Lobbies.Instance.RemovePlayerAsync(lobbyidString, playerId);
            Debug.Log("Player of " + playerId + " has been removed from this room.");
            Logger.Log("Player of " + playerId + " has been removed from this room.");
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.DisconnectLocalClient();
        }
        else if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Lobby and Server will be shutting down.");
            Logger.Log("Lobby and Server will be shutting down.");
            try {

                NetworkManager.Singleton.Shutdown(false);
                await Lobbies.Instance.DeleteLobbyAsync(lobbyidString);
                //NetworkManager.Singleton.SceneManager.LoadScene("LobbyCreateJoin", UnityEngine.SceneManagement.LoadSceneMode.Single);
                Debug.Log("Server has shut down.");
                Logger.Log("Server has shut down.");
                Application.Quit();
            }
            
            catch(Exception error){
                Debug.Log(error);
                Debug.Log("Server failed to shutdown");
                Logger.Log("Server failed to shutdown");
            }

        }
        lobbyidString = "";
    }
   
}
