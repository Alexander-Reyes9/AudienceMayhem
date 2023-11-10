using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//This is just the joining aspect from lobbyoptions, roomtext, and UI Manager Scripts
public class JoinRoomScript : MonoBehaviour
{
    //Network list of type lobby named listOfLobbies
    private static List<Unity.Services.Lobbies.Models.Lobby> listOfLobbies = new List<Unity.Services.Lobbies.Models.Lobby>();
    //number of lobbies active
    public static NetworkVariable<int> roomCount = new NetworkVariable<int>(0);
    public static GameObject lobbiesList;

    //Obtains reference to UnityTransport component, which should be connected to same gameObject as NetworkManager
    public static UnityTransport Transport => NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

    private RelayHostData relayHostData = new RelayHostData();

    private RelayJoinData relayJoinData = new RelayJoinData();

    //Ian's Changes
    [SerializeField]
    private HostClientState stateTracker;


    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        //Debug.Log(AuthenticationService.Instance.PlayerId);
        lobbiesList = GameObject.Find("LobbyList");
        lobbiesList.GetComponent<TextMeshProUGUI>().text = printRoomList();
    }

    //method that makes players join any public lobby 
    public async void JoinRoom(string s)
    {

        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(s);

        //Obtains relay join code
        string joinCode = lobby.Data["joinCode"].Value;
        Debug.Log(joinCode);

        relayJoinData = await RelayManager.JoinRelay(joinCode);

        NetworkManager.Singleton.StartClient();
        stateTracker.IsClient = true;

        /*
        GameObject a = GameObject.Find("JoinLobby");
        RoomText aRoomText = a.GetComponent<RoomText>();
        aRoomText.JoinButton();
        */
    }

    //Method for a player in the JoinRoom scene, to quick join a room without having a code.
    public async void QuickJoin()
    {
        // Quick-join a random lobby with a maximum capacity of 10 or more players.
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();
            Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync(options);
            // Obtains relay join code
            string joinCode = lobby.Data["joinCode"].Value;
            Debug.Log(joinCode);
            relayJoinData = await RelayManager.JoinRelay(joinCode);

            //Ian's Changes
            NetworkManager.Singleton.StartClient();
            stateTracker.IsClient = true;
        }
        finally
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
  
    static async void getRoomList()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            // Filter for open lobbies only
            options.Filters = new List<QueryFilter>()
    {
        new QueryFilter(
            field: QueryFilter.FieldOptions.AvailableSlots,
            op: QueryFilter.OpOptions.GT,
            value: "0")
    };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder>()
    {
        new QueryOrder(
            asc: false,
            field: QueryOrder.FieldOptions.Created)
    };
            
            //Getting list of lobbies active.
            var response = await Lobbies.Instance.QueryLobbiesAsync(options);
            listOfLobbies = response.Results;
        }
        
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    //Returns a string of lobbies in the format of 
    public static string printRoomList()
    {
        getRoomList();
        string lobbyInfo = "";
        for (int i = 0; i < listOfLobbies.Count; i++)
        {
            lobbyInfo += listOfLobbies[i].Name + " " + listOfLobbies[i].Id + "\tMaximum number of players: "+ listOfLobbies[i].MaxPlayers + 
            listOfLobbies[i].AvailableSlots + " spots available for this lobby\n";
        }
        if (lobbyInfo.Equals(""))
            return "No lobbies are currently open.";
        return lobbyInfo;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
