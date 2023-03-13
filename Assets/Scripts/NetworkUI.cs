using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    public GameObject ammoUI, healthUI;
    public void Connect()
    {
        LobbyManager.CreateOrJoinLobby();
        ammoUI.SetActive(true);
        healthUI.SetActive(true);
    }

}
