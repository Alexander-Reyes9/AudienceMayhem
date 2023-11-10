using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ServerRPC : NetworkBehaviour
{
    public GameObject[] networkPrefabs;
    bool hasServerStarted;
    SpawnPickup spawnPickup;

    NetworkingUIManager networkingUIManager;
    AudiencePoll currentQuestion;

    HistoryOfItems historyOfItems;
    GameObject player;


    
    // Start is called before the first frame update
    void Start()
    {
        spawnPickup = GameObject.Find("SpawnArea").GetComponent<SpawnPickup>();
        networkPrefabs = spawnPickup.pickups;
        networkingUIManager = GameObject.Find("UIManager").GetComponent<NetworkingUIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        historyOfItems = player.GetComponent<HistoryOfItems>();

        currentQuestion = networkingUIManager.currentQuestion;

        

    }

    // Update is called once per frame
    void Update()
    {
    }

    void printClientIds()
    {
        Debug.Log(NetworkManager.ConnectedClientsIds);
    }

    [ServerRpc (RequireOwnership = false)]
    public void AddToPlayerInventoryServerRpc(string pickup)
    {
        spawnPickup.SpawnSpecificPickup(pickup);
    }
    

    [ServerRpc (RequireOwnership = false)]
    public void increaseChoiceVoteCountServerRpc(string choice)
    {
        Debug.Log("serverRPC has been called");
        Logger.Log("serverRPC has been called.");
  
        if (choice.ToLower().Equals("choice1"))
        {
            networkingUIManager.choice1VoteCount.Value++;
            Logger.Log($"Choice1 Vote Count: {networkingUIManager.choice1VoteCount.Value}");
            Debug.Log($"Choice1 Vote Count: {networkingUIManager.choice1VoteCount.Value}");
            Debug.Log("Testing");
            SendVoteCountValueClientRpc("choice1", networkingUIManager.choice1VoteCount.Value);

        }
        else if (choice.ToLower().Equals("choice2"))
        {
            networkingUIManager.choice2VoteCount.Value++;
            Logger.Log($"Choice2 Vote Count: {networkingUIManager.choice2VoteCount.Value}");
            Debug.Log($"Choice2 Vote Count: {networkingUIManager.choice2VoteCount.Value}");
            Debug.Log("Testing");
            SendVoteCountValueClientRpc("choice2", networkingUIManager.choice2VoteCount.Value);
        }
        else if (choice.ToLower().Equals("choice3"))
        {
            networkingUIManager.choice3VoteCount.Value++;
            Logger.Log($"Choice3 Vote Count: {networkingUIManager.choice3VoteCount.Value}");
            Debug.Log($"Choice3 Vote Count: {networkingUIManager.choice3VoteCount.Value}");
            Debug.Log("Testing");
            SendVoteCountValueClientRpc("choice3", networkingUIManager.choice3VoteCount.Value);
        }
        else if (choice.ToLower().Equals("choice4"))
        {
            networkingUIManager.choice4VoteCount.Value++;
            Logger.Log($"Choice4 Vote Count: {networkingUIManager.choice4VoteCount.Value}");
            Debug.Log($"Choice4 Vote Count: {networkingUIManager.choice4VoteCount.Value}");
            Debug.Log("Testing");
            SendVoteCountValueClientRpc("choice4", networkingUIManager.choice4VoteCount.Value);
        }

    }

    [ClientRpc]
    public void SendVoteCountValueClientRpc(string choice, int value){
        if(choice.ToLower().Equals("choice1")){
            networkingUIManager.choice1VoteCount.Value = value;
        }
        else if (choice.ToLower().Equals("choice2")){
            networkingUIManager.choice2VoteCount.Value = value;
        }
        else if (choice.ToLower().Equals("choice3")){
            networkingUIManager.choice3VoteCount.Value = value;
        }
        else if (choice.ToLower().Equals("choice4")){
            networkingUIManager.choice4VoteCount.Value = value;
        }
    }

    [ServerRpc (RequireOwnership = false)]
    public void ResetValuesServerRpc()
    {

        for(int i = 0; i < networkingUIManager.listOfVoteCounts.Count; i++)
        {
            networkingUIManager.listOfVoteCounts[i].Value = 0;
            SendVoteCountValueClientRpc("choice" + (i + 1), networkingUIManager.listOfVoteCounts[i].Value);
        }


    }
    

}
