using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using UnityEngine.SceneManagement;

public class MasterLanScript : NetworkManager
{
    [Header("MASTER LAN SCRIPT")]
    [SerializeField] public GameObject SelectedPlayer;

    [Header("Network Storage")]
    [SerializeField] GameObject NetworkStorage;

    [Header("Player UI")]
    [SerializeField] GameObject PlayerHostGo;
    [SerializeField] GameObject PlayerClientGo;
    [SerializeField] GameObject UniversalUI;


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
        PlayerHostGo.SetActive(true);
    }

    public void StartClientLan()
    {
        GetComponent<NetworkDiscoveryHUD>().JoinAsClient();
        PlayerClientGo.SetActive(true);
        UniversalUI.SetActive(true);
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerController.SetActive(true);
        PlayerClientGo.SetActive(true);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().CmdSubReady();
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().CmdDecPlayerTotal();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        //UI ABOUT DISCONNECTION
        
        SceneManager.LoadScene(MainMenu);
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
        string sceneName = NetworkStorage.GetComponent<NetworkStorage>().MapName;
        switch (sceneName)
        {
            case "Stage1":
                sceneName = "Stage1LanScene";
                break;
            case "Stage2":
                sceneName = "Stage2LanScene";
                break;
            case "Stage3":
                sceneName = "Stage3LanScene";
                break;
            case "Lobby":
                sceneName = "MasterLanLobby";
                break;
            default:
                Debug.Log("Map does not exist");
                break;
        }
        RunDefaultUi();
        ServerChangeScene(sceneName);
    }

    public void ChangeServerScene(string name)
    {
        RunDefaultUi();
        ServerChangeScene(name);
    }
}
