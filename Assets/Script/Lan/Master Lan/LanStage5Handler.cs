using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LanStage5Handler : NetworkBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject clearnBtn;
    [SerializeField] public GameObject pickUP;
    [SerializeField] public GameObject controller;

    [SerializeField] public GameObject GivenTvText;
    [SerializeField] public GameObject GoalTvText;
    [SerializeField] public GameObject ReadyTimerText;

    [SerializeField] public List<GameObject> PowerUpsGo;

    public int maxSpawn = 10;
    public bool iAmWinner = false;

    [SyncVar]
    public int currentSpawn = 0;

    [SyncVar(hook = nameof(isWinnerHook))]
    public bool isGameOver = false;


    public SyncList<int> correctKeyList = new SyncList<int>();
    public SyncDictionary<int, int> correctArrangement = new SyncDictionary<int, int>();

    [SerializeField] public GameObject CollectMeObj;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        clearnBtn.SetActive(false);
        pickUP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAllUi()
    {
        S5GoStoreScript Lan5SceneStore = GameObject.Find("S5GoStorage").GetComponent<S5GoStoreScript>();
        GivenTvText = Lan5SceneStore.GivenTvText;
        GoalTvText = Lan5SceneStore.GoalTvText;
    }

    public void OffUi()
    {
        clearnBtn.SetActive(false);
        pickUP.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdGenerateGiven()
    {
        if (isServer)
        {
            correctArrangement.Clear();
            for (int i = 0; i < 5; i++)
            {
                correctArrangement.Add(i, Random.Range(1, 20));
            }
            List<int> keyList = new List<int>
            {
                0,
                1,
                2,
                3,
                4
            };
            for (int i = 0; i < 5; i++)
            {
                int ran = Random.Range(0, keyList.Count);
                correctKeyList.Add(keyList[ran]);
                keyList.Remove(keyList[ran]);
            }
            string collectedData = $"Array Value\n{{{correctArrangement[correctKeyList[0]]}, {correctArrangement[correctKeyList[1]]}, {correctArrangement[correctKeyList[2]]}, {correctArrangement[correctKeyList[3]]}, {correctArrangement[correctKeyList[4]]}}}";
            // string correctArrangementData = $"[{correctArrangement[0]}, {correctArrangement[1]}, {correctArrangement[2]}, {correctArrangement[3]}, {correctArrangement[4]}]";
            string correctArrangementData = $"Index\n[{correctKeyList[0]}], [{correctKeyList[1]}], [{correctKeyList[2]}], [{correctKeyList[3]}], [{correctKeyList[4]}]";
            Debug.Log($"{correctKeyList[0]} , {correctKeyList[1]}, {correctKeyList[2]}, {correctKeyList[3]}, {correctKeyList[4]}");
            RpcChangeTvScreen(collectedData, correctArrangementData);
        }
    }

    IEnumerator countdownCor()
    {
        RpcSetTimerVisible(true);
        while (int.Parse(ReadyTimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if(int.Parse(ReadyTimerText.GetComponent<TextMeshProUGUI>().text) <= 1)
            {
                RpcSetTimerVisible(false);
                RpcResetS5Ui();
                CmdGenerateGiven();
            }
            int sec = int.Parse(ReadyTimerText.GetComponent<TextMeshProUGUI>().text);
            sec--;
            RpcUpdateTimerUi(sec.ToString());
            yield return new WaitForSeconds(1f);
        }
    }

    [ClientRpc]
    public void RpcResetS5Ui()
    {
        pickUP.SetActive(true);
        clearnBtn.SetActive(true);
    }


    [ClientRpc]
    public void RpcSetTimerVisible(bool status)
    {
        ReadyTimerText.SetActive(status);
    }

    [ClientRpc]
    public void RpcUpdateTimerUi(string sec)
    {
        ReadyTimerText.GetComponent<TextMeshProUGUI>().text = sec;
    }

    [ClientRpc]
    public void RpcChangeTvScreen(string collectedData, string correctArrangementData)
    {
        GivenTvText.GetComponent<TextMeshPro>().text = collectedData;
        GoalTvText.GetComponent<TextMeshPro>().text = correctArrangementData;
    }

    public void MasterStart()
    {
        if (isServer)
        {
            RpcSpawnObject();
            StartCoroutine(CheckCurrent());
            StartCoroutine(countdownCor());
            StartCoroutine(SpawnPowerUpsCor());
        }
    }

    IEnumerator CheckCurrent()
    {
        while (true)
        {
            if (isServer)
            {
                RpcSpawnObject();
            }
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    public void GetAllPowerUps()
    {
        NetworkStorage NetworkStore = GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>();
        PowerUpsGo.Add(NetworkStore.PowerUpResetCd);
        PowerUpsGo.Add(NetworkStore.PowerUpSpeed);
        PowerUpsGo.Add(NetworkStore.PowerUpUlti);
    }

    IEnumerator SpawnPowerUpsCor()
    {
        while (true)
        {
            if (isServer)
            {
                Debug.Log("Spawning PowerUps!");
                int rand = Random.Range(0, PowerUpsGo.Count);
                GameObject Go = Instantiate(PowerUpsGo[rand], new Vector3(Random.Range(-5.8f, 54.34f), 1.62f, Random.Range(-8.7f, 54.32f)), Quaternion.identity);
                NetworkServer.Spawn(Go, player.GetComponent<PlayerLanExtension>().connectionToClient);
            }
            yield return new WaitForSeconds(Random.Range(8, 15));
        }
    }

    [ClientRpc]
    public void RpcSpawnObject()
    {
        if (isServer)
        {
            //Min x = -5.8 max 54.34
            //max z = 54.32 min = -8.7
            //y = 0.9400001
            for (int i = 0; i < maxSpawn - currentSpawn; i++)
            {
                GameObject LanCollectObj = Instantiate(CollectMeObj, new Vector3(Random.Range(-5.8f, 54.34f), 1.8f, Random.Range(-8.7f, 54.32f)), Quaternion.identity);
                currentSpawn++;
                NetworkServer.Spawn(LanCollectObj, player.GetComponent<PlayerLanExtension>().connectionToClient);
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdDecCurrentSpawn()
    {
        currentSpawn--;
    }

    public void DeclareWinner()
    {
        iAmWinner = true;
        CmdIsGameRunning(true);
    }

    [Command(requiresAuthority = false)]
    public void CmdIsGameRunning(bool status)
    {
        isGameOver = status;
    }

    public void isWinnerHook(bool oldValue, bool newValue)
    {
        if (isGameOver)
        {
            if (iAmWinner)
            {
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().CongratsGo.SetActive(true);
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().DisableCoroutine();
            }
            else
            {
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().ThankYouGo.SetActive(true);
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().DisableCoroutine();
            }
            if (isServer && isLocalPlayer)
            {
                Invoke(nameof(DisconnectServer), 5f);
            }
            if (!isServer && isLocalPlayer)
            {
                Invoke(nameof(DisconnectPlayer), 3f);
            }
        }
    }

    public void DisconnectPlayer()
    {
        NetworkManager.singleton.StopClient();
    }

    public void DisconnectServer()
    {
        NetworkManager.singleton.StopHost();
    }

}
