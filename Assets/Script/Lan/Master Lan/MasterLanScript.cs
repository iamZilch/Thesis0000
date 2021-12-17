using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using UnityEngine.SceneManagement;
using System;

public class MasterLanScript : NetworkManager
{
    [Header("MASTER LAN SCRIPT")]
    [SerializeField] public GameObject SelectedPlayer;

    [Header("To Be Destroy")]
    [SerializeField] public GameObject[] tbdObj;

    [Header("Network Storage")]
    [SerializeField] GameObject NetworkStorage;

    [Header("Player UI")]
    [SerializeField] GameObject PlayerHostGo;
    [SerializeField] GameObject PlayerClientGo;
    [SerializeField] GameObject UniversalUI;

    [Header("Player")]
    [SerializeField] public GameObject player;

    [Header("Scene List")]
    [Scene]
    public string MainMenu;
    [Scene]
    public string lobbyLan;
    [Scene]
    public string stage1Scene;
    [Scene]
    public string stage2Scene;
    [Scene]
    public string stage3Scene;


    public override void Start()
    {
        base.Start();
        //Get the current player that is selected by the user. For now lez just do predefine value
        //singleton.playerPrefab = GameObject.Find("MyPlayerPrefabs").GetComponent<PlayerPrefab>().getPrefab(GameObject.Find("Opening_Game_Script").GetComponent<Database>().UsedCharacter);
        singleton.playerPrefab = SelectedPlayer;
        RunDefaultUi();
    }

    public void RunDefaultUi()
    {
        PlayerHostGo.SetActive(false);
        PlayerClientGo.SetActive(false);
        UniversalUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<NetworkDiscoveryHUD>().JoinAsClient();
            PlayerClientGo.SetActive(true);
            UniversalUI.SetActive(true);
        }
    }

    public void StartHostLan()
    {
        GetComponent<NetworkDiscoveryHUD>().StartHostLan();
        PlayerHostGo.SetActive(true);
        UniversalUI.SetActive(true);
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerController.SetActive(true);
    }

    public void StartClientLan()
    {
        GetComponent<NetworkDiscoveryHUD>().JoinAsClient();
        PlayerClientGo.SetActive(true);
        UniversalUI.SetActive(true);
        try
        {
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerController.SetActive(true);
        }
        catch (Exception e) { }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        /*if(player.GetComponent<PlayerLanExtension>().isServer && player.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            for (int i = 0; i < NetworkStorage.GetComponent<NetworkStorage>().playerLan.Count; i++)
            {
                if (NetworkStorage.GetComponent<NetworkStorage>().playerLan[i].GetComponent<NetworkIdentity>().connectionToClient == conn)
                {
                    Debug.Log("isReady : " + NetworkStorage.GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().isReady);
                    if (NetworkStorage.GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().isReady)
                    {
                        NetworkStorage.GetComponent<NetworkStorage>().CmdSubReady();
                    }
                    NetworkStorage.GetComponent<NetworkStorage>().CmdRemoveQuitPlayer(NetworkStorage.GetComponent<NetworkStorage>().playerLan[i]);
                    break;
                }
            }
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().CmdDecPlayerTotal();
        }*/
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        if (player.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            for (int i = 0; i < tbdObj.Length; i++)
            {
                Destroy(tbdObj[i], 1f);
            }
            SceneManager.LoadScene(MainMenu);
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (player.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            for (int i = 0; i < tbdObj.Length; i++)
            {
                Destroy(tbdObj[i], 1f);
            }
            SceneManager.LoadScene(MainMenu);
        }
    }


    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        if (!NetworkClient.ready)
        {
            NetworkClient.ready = true;
        }
        NetworkStorage.GetComponent<NetworkStorage>().playerLan.Clear();
        NetworkStorage.GetComponent<NetworkStorage>().PlayerTotal = 0;
        NetworkStorage.GetComponent<NetworkStorage>().PositionPos = 0;
    }

    public void ChangeServerScene()
    {
        singleton.maxConnections = NetworkStorage.GetComponent<NetworkStorage>().PlayerTotal;
        string sceneName = GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().MapName;
        switch (sceneName)
        {
            case "HEXA-Types":
                sceneName = "Stage1LanScene";
                break;
            case "Operation: De-Bug!":
                sceneName = "Stage2LanScene";
                break;
            case "De-Slide!":
                sceneName = "Stage3LanScene";
                break;
            case "Disk-O!":
                sceneName = "Stage4LanScene";
                break;
            case "Collect Me If I'm Wrong":
                sceneName = "Stage5LanScene";
                break;
            case "Lobby":
                sceneName = "MasterLanLobby";
                break;
            default:
                Debug.Log("Map does not exist");
                break;
        }
        NetworkStorage.GetComponent<NetworkStorage>().CmdResetMasterLanDefaultUi();
        ServerChangeScene(sceneName);
    }

    public void ChangeServerScene(string name)
    {
        RunDefaultUi();
        ServerChangeScene(name);
    }
}
