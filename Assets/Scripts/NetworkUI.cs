using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    public void Connect()
    {
        LobbyManager.CreateOrJoinLobby();
    }

}
