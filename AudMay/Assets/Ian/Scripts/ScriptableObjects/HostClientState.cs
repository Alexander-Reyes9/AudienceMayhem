using UnityEngine;

[CreateAssetMenu(fileName = "NewStateTracker", menuName = "HostClientState")]
public class HostClientState : ScriptableObject
{
    public bool IsHost;
    public bool IsClient;

    public bool IsServer;

    public string hostRoomId;
}
