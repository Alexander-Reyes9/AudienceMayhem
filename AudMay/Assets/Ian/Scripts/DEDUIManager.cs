using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Netcode;
public class DEDUIManager : MonoBehaviour
{
    [SerializeField]
    private Button replayBtn;

    [SerializeField]
    private HostClientState stateTracker;
    // Start is called before the first frame update
    void Start()
    {
        replayBtn.onClick.AddListener(async () =>
        {
            if(stateTracker.IsHost){
                await Lobbies.Instance.DeleteLobbyAsync(stateTracker.hostRoomId);
                Destroy(GameObject.Find("NetworkManager"));
                ResetStateTrackerValues();
                SceneManager.LoadScene("DecisionScene");    
            }
            else if(stateTracker.IsClient){
                Destroy(GameObject.Find("NetworkManager"));
                ResetStateTrackerValues();
                SceneManager.LoadScene("DecisionScene");
            }


            
        });
    }

    void ResetStateTrackerValues(){
        stateTracker.IsHost = false;
        stateTracker.IsClient = false;
        stateTracker.IsServer = false;
        stateTracker.hostRoomId = null;
    }

    void OnApplicationQuit()
    {
        //Resets state trackers' booleans when playmode is stopped in editor or when application is quit
        stateTracker.IsHost = false;
        stateTracker.IsClient = false;
        stateTracker.IsServer = false;
    }

    
}
