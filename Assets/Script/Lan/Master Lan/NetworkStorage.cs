using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkStorage : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject MapNameTxt;
    [SerializeField] GameObject PlayerReadyTxt;
    [SerializeField] GameObject PlayerTotalTxt;
    [SerializeField] GameObject PlayerReadyButton;
    [SerializeField] public GameObject Player;

    [Header("Prefabs")]
    [SerializeField] public GameObject PowerUpUlti;
    [SerializeField] public GameObject PowerUpSpeed;
    [SerializeField] public GameObject PowerUpResetCd;

    [SerializeField] public GameObject PlayerController;

    [SerializeField] public GameObject ThankYouGo;
    [SerializeField] public GameObject CongratsGo;
    [SerializeField] public GameObject DisconnectedGo;

    [Header("Variables")]
    private string[] mapNames = {"Stage1", "Stage2", "Stage3", "Stage4"};

    [Header("Enviroment Testing")]
    [SerializeField] public GameObject[] powerUpsSpawnPoint;
    
    [SyncVar]
    public int mapNameInt = 0;

    public SyncList<GameObject> playerLan = new SyncList<GameObject>();

    [SyncVar(hook =nameof(MapNameHook))]
    public string MapName = "Stage1";

    [SyncVar(hook = nameof(PlayerReadyHook))]
    public int PlayerReady = 0;

    [SyncVar(hook = nameof(PlayerTotalHook))]
    public int PlayerTotal = 0;

    [SyncVar(hook =nameof(PositionPostHook))]
    public int PositionPos = 0;

    public void PositionPostHook(int oldValue, int newValue)
    {
        PositionPos = newValue;
        if (GameObject.Find("PlayerPositionHandler"))
        {
            GameObject.Find("PlayerPositionHandler").GetComponent<PositionHandler>().pos = newValue;
        }
    }

    public void PlayerTotalHook(int oldValue, int newValue)
    {
        PlayerTotal = newValue;
        PlayerTotalTxt.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void PlayerReadyHook(int oldValue, int newValue)
    {
        PlayerReady = newValue;
        PlayerReadyTxt.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void MapNameHook(string oldValue, string newValue)
    {
        MapName = newValue;
        MapNameTxt.GetComponent<TextMeshProUGUI>().text = newValue;
    }


    private void Start()
    {
        playerLan = new SyncList<GameObject>();
        DontDestroyOnLoad(this);
    }


    public void RpcHandleReady()
    {
        if (PlayerReadyButton.GetComponent<TextMeshProUGUI>().text.Equals("Ready"))
        {
            PlayerReadyButton.GetComponent<TextMeshProUGUI>().text = "Unready";
            Player.GetComponent<PlayerLanExtension>().CmdSetReadyLocal(true);
            CmdAddReady();
        }
        else if(PlayerReadyButton.GetComponent<TextMeshProUGUI>().text.Equals("Unready"))
        {
            PlayerReadyButton.GetComponent<TextMeshProUGUI>().text = "Ready";
            Player.GetComponent<PlayerLanExtension>().CmdSetReadyLocal(false);
            CmdSubReady();
        }
    }

   [ClientRpc]
   public void RpcSpawnTestPowerUps()
    {
        if (isServer)
        {
            GameObject ult = Instantiate(PowerUpUlti);
            ult.transform.position = powerUpsSpawnPoint[0].transform.position;
            GameObject speedUp = Instantiate(PowerUpSpeed);
            speedUp.transform.position = powerUpsSpawnPoint[2].transform.position;
            GameObject resetCd = Instantiate(PowerUpResetCd);
            resetCd.transform.position = powerUpsSpawnPoint[1].transform.position;
            NetworkServer.Spawn(ult, Player.GetComponent<NetworkIdentity>().connectionToClient);
            NetworkServer.Spawn(speedUp, Player.GetComponent<NetworkIdentity>().connectionToClient);
            NetworkServer.Spawn(resetCd, Player.GetComponent<NetworkIdentity>().connectionToClient);
        }
    }

    [Command(requiresAuthority =false)]
    public void CmdResetMasterLanDefaultUi()
    {
        RpcResetMasterLanDefaultUi();
    }

    [ClientRpc]
    public void RpcResetMasterLanDefaultUi()
    {
        GameObject.Find("NetworkManager").GetComponent<MasterLanScript>().RunDefaultUi();
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveQuitPlayer(GameObject go)
    {
        playerLan.Remove(go);
    }

    [Command(requiresAuthority = false)]
    private void CmdSetDefaultMap()
    {
        MapName = "Stage1";
    }

    [Command(requiresAuthority =false)]
    public void CmdAddReady()
    {
        PlayerReady++;
    }

    [Command(requiresAuthority = false)]
    public void CmdSubReady()
    {
        PlayerReady--;
    }

    [Command(requiresAuthority = false)]
    public void CmdDecPlayerTotal()
    {
        PlayerTotal--;
    }

    public void CmdChangeMap()
    {
        MapName = mapNames[mapNameInt];
        mapNameInt++;
        if(mapNameInt >= mapNames.Length)
        {
            mapNameInt = 0;
        }
    }

    [Command(requiresAuthority = false)]
    public void resetScene()
    {
        if (isServer)
        {
            GameObject.Find("NetworkManager").GetComponent<MasterLanScript>().ChangeServerScene("Main_Menu_Scene");
        }
    }

    public void DisableCoroutine()
    {
        StartCoroutine(DisableLocalMsg());
    }

    IEnumerator DisableLocalMsg()
    {
        yield return new WaitForSeconds(3f);
        CongratsGo.SetActive(false);
        ThankYouGo.SetActive(false);
        DisconnectedGo.SetActive(false);
        resetScene();
        StopAllCoroutines();
    }

}
