using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class ViewManager : MonoBehaviour
{
    
    private GameObject playerView;
    private GameObject audienceView;

    AudiencePollLibrary audiencePollLibrary;
    private List<AudiencePoll> pollLibrary;

    [SerializeField]
    private HostClientState stateTracker;

    void Awake(){
        playerView = GameObject.Find("PlayerView");
        audienceView = GameObject.Find("AudienceView");

        if(stateTracker.IsClient){
            playerView.SetActive(false);
            audiencePollLibrary = GameObject.FindGameObjectWithTag("AudiencePollManager").GetComponent<AudiencePollLibrary>(); 
            pollLibrary = audiencePollLibrary.libraryOfPolls;
            audiencePollLibrary.CreateQuestionSets();
 
        }
        else if (stateTracker.IsHost)
        {
            audienceView.SetActive(false);
        }
        
    }

    void OnApplicationQuit()
    {
        //Resets state trackers' booleans when playmode is stopped in editor or when application is quit
        stateTracker.IsHost = false;
        stateTracker.IsClient = false;
        stateTracker.IsServer = false;
    }

}
