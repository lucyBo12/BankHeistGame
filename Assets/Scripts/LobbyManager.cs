using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using UnityEngine;

public static class LobbyManager
{
    public static Lobby Current { get; private set;}
    private static QueryResponse QR { get; set; }
    private static UnityTransport Transport => GameObject.FindObjectOfType<UnityTransport>();
    private const string JoinCodeKey = "j";
    public static string PlayerName { get; set; }
    public static string PlayerID => AuthenticationService.Instance.PlayerId;


    public static async void CreateOrJoinLobby() {
        await Authenticate();
        Current = await QuickJoinLobby() ?? await CreateLobby();
    }

    private static async Task Authenticate() {
        var options = new InitializationOptions();
        await UnityServices.InitializeAsync(options);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private static async Task<Lobby> QuickJoinLobby()
    {
        try
        {
            var lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
            var a = await RelayService.Instance.JoinAllocationAsync(lobby.Data[JoinCodeKey].Value);

            //SetTransformAsClient(a)

            NetworkManager.Singleton.StartClient();
            return lobby;
        }
        catch (Exception e) { 
            Debug.LogException(e);
            Debug.LogError("No lobbies available via quick join");
            return null;
        }
    }

    private static async Task<Lobby> CreateLobby()
    {
        try
        {
            Debug.Log(">1");
            const int maxPlayers = 5;
            var a = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
            Debug.Log(">2");
            var options = new CreateLobbyOptions()
            {
                Data = new Dictionary<string, DataObject> { { JoinCodeKey, new DataObject(DataObject.VisibilityOptions.Public, joinCode) } }
            };
            var lobby = await Lobbies.Instance.CreateLobbyAsync("Dolefish", maxPlayers, options);
            Debug.Log(">3");
            Timeout(lobby.Id, DateTime.Now.AddMinutes(10));

            Debug.Log(">4");
            NetworkManager.Singleton.StartHost();
            Transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

            return lobby;
        }
        catch (Exception e) {
            Debug.LogError($"Could not create lobby \n{e}");
            return null;
        }
    }

    private static async void Timeout(string lobbyId, DateTime expiry) {
        await Task.Delay(10000);
        if (!Application.isPlaying) return;
        if (DateTime.Now > expiry) return;

        await Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
        Timeout(lobbyId, expiry);
    }
}