using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class NetworkingUIManager : MonoBehaviour
{
    [SerializeField]
    private Button startHost;

    [SerializeField]
    private Button startClient;

    [SerializeField]
    private TMP_InputField joinCode;


    //<summary>
    //Fields of the UI elements for the AudienceCanvas
    [SerializeField]
    private TMP_Text questionTextBox;

    [SerializeField]
    private GameObject choice1GameObject;
    private Button choice1Btn;
    private TMP_Text choice1Text;

    [SerializeField]
    private GameObject choice2GameObject;
    private Button choice2Btn;
    private TMP_Text choice2Text;

    [SerializeField]
    private GameObject choice3GameObject;
    private Button choice3Btn;
    private TMP_Text choice3Text;
    
    [SerializeField]
    private GameObject choice4GameObject;
    private Button choice4Btn;
    private TMP_Text choice4Text;


    


    private ServerRPC serverRPC;

    private List<AudiencePoll> pollLibrary;

    public AudiencePoll currentQuestion;

    private bool firstQuestion;

    //<description>
    //NetworkVariable that manages the question textbox for all audience members
    //<end>
    private NetworkVariable<FixedString64Bytes> questionNetworkVariable = new NetworkVariable<FixedString64Bytes>(NetworkVariableReadPermission.Everyone, new FixedString64Bytes("Why does this code stink?")); 

    //<description>
    //NetworkVariables that manage the choiceButtons' text for all audience members
    //<end>
    private NetworkVariable<FixedString64Bytes> choice1NetworkString = new NetworkVariable<FixedString64Bytes>();

    private NetworkVariable<FixedString64Bytes> choice2NetworkString = new NetworkVariable<FixedString64Bytes>();

    private NetworkVariable<FixedString64Bytes> choice3NetworkString = new NetworkVariable<FixedString64Bytes>();

    private NetworkVariable<FixedString64Bytes> choice4NetworkString = new NetworkVariable<FixedString64Bytes>();

    //<description>
    //NetworkVariables that manage the voteCount of each choice for all audience members
    //<end>
    public NetworkVariable<int> choice1VoteCount = new NetworkVariable<int>();

    public NetworkVariable<int> choice2VoteCount = new NetworkVariable<int>();

    public NetworkVariable<int> choice3VoteCount = new NetworkVariable<int>();

    public NetworkVariable<int> choice4VoteCount = new NetworkVariable<int>();

    Dictionary<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>> choices = new Dictionary<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>>();

    public List<NetworkVariable<int>> listOfVoteCounts;

    //private string debugPanel;

    public GameObject audienceCanvas;

    //<summary>
    //This is a scuffed canvas used to just to initialize host and client
    //Host and client will actually be started in the lobby scene; host will then use networkscenemanager to switch to RelayIntegration Scene
    //<summary>
    public GameObject scuffedCanvas;


    //Used to check if any audiencemembers have voted.
    private bool audienceMembersVoted;

    private bool giveQuestion;

    [SerializeField]
    private HostClientState stateTracker;

    void Start()
    {
        choice1Btn = choice1GameObject.GetComponent<Button>();
        choice1Text = choice1GameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        choice2Btn = choice2GameObject.GetComponent<Button>();
        choice2Text = choice2GameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        choice3Btn = choice3GameObject.GetComponent<Button>();
        choice3Text = choice3GameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        choice4Btn = choice4GameObject.GetComponent<Button>();
        choice4Text = choice4GameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        serverRPC = GameObject.FindGameObjectWithTag("RPCManager").GetComponent<ServerRPC>();

        //<summary>
        //Configures dictionary of NetworkVariables that describe each choice's name and vote count.
        //<summary>
        choices.Add(choice1NetworkString, choice1VoteCount);
        choices.Add(choice2NetworkString, choice2VoteCount);
        choices.Add(choice3NetworkString, choice3VoteCount);
        choices.Add(choice4NetworkString, choice4VoteCount);

        listOfVoteCounts.Add(choice1VoteCount);
        listOfVoteCounts.Add(choice2VoteCount);
        listOfVoteCounts.Add(choice3VoteCount);
        listOfVoteCounts.Add(choice4VoteCount);


        questionTextBox.text = questionNetworkVariable.Value.Value;

        audienceMembersVoted = false;

        giveQuestion = true;
        firstQuestion = true;
        if (stateTracker.IsClient)
        {
            pollLibrary = GameObject.FindGameObjectWithTag("AudiencePollManager").GetComponent<AudiencePollLibrary>().libraryOfPolls;
            audienceCanvas.SetActive(true);
            StartCoroutine(giveAudienceQuestion(10));
        }
        else if (stateTracker.IsHost)
        {
            audienceCanvas.SetActive(false);
        }
        


        //<Summary>
        //Add listeners to buttons when clicked - develops asyncronous callback to start server, host, or client
        //<Summary>
        /*startHost.onClick.AddListener(async () =>
        {
            Debug.Log("Start Host Button was clicked.");
            Logger.Log("Start Host Button was clicked.");
            if (NetworkManager.Singleton.IsServer)
            {
                Debug.Log("Server already running!");
                Logger.Log("Server already running!");
                return;
            }
            else
            {
                if (RelayManager.IsRelayEnabled)
                {
                    await RelayManager.SetupRelay();
                }
                NetworkManager.Singleton.StartHost();
            }
            scuffedCanvas.SetActive(false);
            audienceCanvas.SetActive(true);
        });

        startClient.onClick.AddListener(async () =>
        {
            Debug.Log("Start Client Button was clicked.");
            Logger.Log("Start Client Button was clicked.");
            if (NetworkManager.Singleton.IsClient && NetworkManager.Singleton.IsConnectedClient)
            {
                Debug.Log("Client already connected to server!");
                Logger.Log("Client already connected to server!");
                return;
            }
            else
            {
                if (RelayManager.IsRelayEnabled)
                {
                    await RelayManager.JoinRelay(joinCode.text);
                }
                scuffedCanvas.SetActive(false);
                audienceCanvas.SetActive(true);
                NetworkManager.Singleton.StartClient();
                giveQuestion = true;
                firstQuestion = true;
                StartCoroutine(giveAudienceQuestion(10));
                
            }
        });
        */


        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            Debug.Log($"{id} just connected to the server.");
            Logger.Log($"{id} just connected to the server.");
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            Debug.Log($"{id} disconnected from the server.");
            Logger.Log($"{id} disconnected from the server.");
        };


    }


    /*private void OnGUI()
    {
        debugPanel = GUI.TextArea(new Rect(0, 0, Screen.width - Screen.width/2, Screen.height - Screen.height/2), Logger.document);

    }
    */


    public void spawnPickupBtnClicked(string pickup)
    {
        serverRPC.AddToPlayerInventoryServerRpc(pickup);
    }

    public void choiceBtnClicked(GameObject choice)
    {
        if(choice.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text.Length > 0)
        {
            serverRPC.increaseChoiceVoteCountServerRpc(choice.name);
            audienceMembersVoted = true;
        }

        
    }


    #region "AudienceCode"

    IEnumerator giveAudienceQuestion(int waitTime){
        while (giveQuestion)
        {
            Debug.Log("New Question will be given");
            Logger.Log("New Question will be given");

            //Picks random question to display
            if (firstQuestion)
            {
                currentQuestion = pollLibrary[0];
                firstQuestion = false;
                pollLibrary.RemoveAt(0);
            }
            else
            {
                int i = Random.Range(0, pollLibrary.Count);
                currentQuestion = pollLibrary[i];
            }

            //Storing the actual question such as "What weapon should the player have?" in temporary string because currentQuestion will change the string afterward.
            string originalQuestion = currentQuestion.question;

            serverRPC.ResetValuesServerRpc();
            ConfigureNetworkAudienceValues();

            //Waits for <param> waitTime amount of seconds before determining which choice was chosen
            yield return new WaitForSeconds(waitTime);

            Logger.Log($"{audienceMembersVoted}");
            if (audienceMembersVoted)
            {
                KeyValuePair<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>> highestVotePair = DeterminePollResults();

                NetworkVariable<int> highestCount = highestVotePair.Value;
                

                FixedString64Bytes winningChoice = highestVotePair.Key.Value;
               
                //Changes the actual question's value.
                currentQuestion.question = $"The choice: {winningChoice.Value} has been chosen";
                ConfigureNetworkAudienceValues();
                ApplyChoiceEffect(winningChoice.Value);

                yield return new WaitForSeconds(5);

            }

            audienceMembersVoted = false;
            

            currentQuestion.question = originalQuestion;

            serverRPC.ResetValuesServerRpc();
        }
        
        

    }

    public void ConfigureLocalAudienceUI(){

        questionTextBox.text = questionNetworkVariable.Value.Value;
        choice1Text.text = choice1NetworkString.Value.Value;
        choice2Text.text = choice2NetworkString.Value.Value;
        choice3Text.text = choice3NetworkString.Value.Value;
        choice4Text.text = choice4NetworkString.Value.Value;

    }

    private void ConfigureNetworkAudienceValues(){

        questionNetworkVariable.Value = currentQuestion.question;
        choice1NetworkString.Value = currentQuestion.choice1.text;
        choice2NetworkString.Value = currentQuestion.choice2.text;
        choice3NetworkString.Value = currentQuestion.choice3.text;
        choice4NetworkString.Value = currentQuestion.choice4.text;

        
        ConfigureLocalAudienceUI();
    }

    private KeyValuePair<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>> DeterminePollResults()
    {

        string choiceName = "placeholder";
        NetworkVariable<int> highestVote = new NetworkVariable<int>(-1);

        foreach(KeyValuePair<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>> vote in choices )
        {

            if(vote.Value.Value > highestVote.Value)
            {
                highestVote.Value = vote.Value.Value;
                choiceName = vote.Key.Value.Value;
                
            }

            
        }

        Logger.Log($"The choice {choiceName} has the highest vote count of: {highestVote.Value.ToString()}");
        Debug.Log($"The choice {choiceName} has the highest vote count of: {highestVote.Value}");

        KeyValuePair<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>> highestVotePair = new KeyValuePair<NetworkVariable<FixedString64Bytes>, NetworkVariable<int>>
            (new NetworkVariable<FixedString64Bytes>(choiceName), highestVote);

        return highestVotePair;
    }

    private void ApplyChoiceEffect(string choice)
    {
        Choice[] listOfChoices = new Choice[] { currentQuestion.choice1, currentQuestion.choice2, currentQuestion.choice3, currentQuestion.choice4 };

        Choice choiceToAffectPlayer = (Choice)ScriptableObject.CreateInstance(typeof (Choice));
        foreach(Choice c in listOfChoices)
        {
            if(c.text == choice)
            {
                choiceToAffectPlayer = c;
            }
        }

        if (choiceToAffectPlayer.canSpawnObject)
        {
            Debug.Log(choiceToAffectPlayer.objectToSpawn);
            Logger.Log(choiceToAffectPlayer.objectToSpawn);
            serverRPC.AddToPlayerInventoryServerRpc(choiceToAffectPlayer.objectToSpawn);
        }
        else if (choiceToAffectPlayer.canChangeRooms)
        {

        }

    }


    #endregion
}
