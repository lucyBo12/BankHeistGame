using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private UnityTransport transport;


    private void Awake()
    {
        hostBtn.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartClient();
        });
    }

    public void Connect()
    {
        LobbyManager.CreateOrJoinLobby();
    }

}
