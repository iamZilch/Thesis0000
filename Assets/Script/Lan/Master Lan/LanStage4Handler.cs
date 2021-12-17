using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LanStage4Handler : NetworkBehaviour
{
    [SerializeField] GameObject ReadyTimerGo;
    [SerializeField] GameObject GivenGo;
    [SerializeField] GameObject PathGo;
    [SerializeField] GameObject[] Answers;
    [SerializeField] GameObject TVText;

    [SerializeField] GameObject[] loopFirst;
    [SerializeField] GameObject[] loopSecond;
    [SerializeField] GameObject[] loopThird;
    [SerializeField] GameObject[] loopFourth;
    [SerializeField] GameObject[] loopFifth;

    [SerializeField] public GameObject Player;
    [SerializeField] public List<GameObject> PowerUpsGo;

    [SyncVar(hook = nameof(ReadyTimerIntHook))]
    public int ReadyTimerInt = 4;

    [SyncVar(hook = nameof(GivenStrHook))]
    public string GivenStr = "";

    [SyncVar(hook = nameof(correctAnswerHook))]
    public int correctAnswer = 0;

    [SyncVar(hook = nameof(isWinnerHook))]
    public bool isWinnerBool = false;

    public bool iAmWinner = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void MasterStart()
    {
        if (isServer)
        {
            CmdResetValue();
            StartCoroutine(ReadyTimerNumerator());
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdStartCour()
    {
        if (isServer)
        {
            StartCoroutine(ReadyTimerNumerator());
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
                int rand = UnityEngine.Random.Range(0, PowerUpsGo.Count);
                GameObject Go = Instantiate(PowerUpsGo[rand], new Vector3(UnityEngine.Random.Range(385.1f, 516f), 1.62f, UnityEngine.Random.Range(-0.5f, 55.3f)), Quaternion.identity);
                NetworkServer.Spawn(Go, Player.GetComponent<PlayerLanExtension>().connectionToClient);
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(8, 15));
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdResetValue()
    {
        if (isServer)
        {
            ReadyTimerInt = 4;
            GivenStr = "";
            correctAnswer = 0;
            isWinnerBool = false;
        }
    }

    IEnumerator ReadyTimerNumerator()
    {
        CmdDefaultUi(false);
        while (!isWinnerBool)
        {
            if (ReadyTimerInt <= 1)
            {
                StopAllCoroutines();
                CmdDefaultUi(true);
                CmdGenerateGiven();
                StartCoroutine(SpawnPowerUpsCor());
            }
            CmdDecTimer();
            yield return new WaitForSeconds(1f);
        }
    }

    [ClientRpc(includeOwner = false)]
    public void RpcDecTimeDis()
    {
        ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = ReadyTimerInt.ToString();
    }

    [Command(requiresAuthority = false)]
    public void CmdDecTimer()
    {
        ReadyTimerInt--;
        RpcDecTimeDis();
    }

    [Command(requiresAuthority = false)]
    public void CmdDefaultUi(bool status)
    {
        if (isServer)
        {
            RpcDefaultUI(status);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdGenerateGiven()
    {
        if (isServer)
        {
            GenerateGivenForLoop();
        }
    }

    public void GenerateGivenForLoop()
    {
        Dictionary<int, string> given = new Dictionary<int, string>
        {
            { 0, "If n = 0\n\tand you repeat \n\tn = n + 2, 5 times" },
            { 1, "If n = 2\n\tand you repeat \n\tn = n * 2, 5 times." },
            { 2, "If n = 1\n\tand you repeat \n\tn = n + n, 5 times. " },
            { 3, "If n = 10\n\tand you repeat \n\tn = n - 2, 5 times. " },
            { 4, "If n = 1\n\tand you repeat \n\tn = (n * 2) - n, 5 times. " }
        };

        Dictionary<int, int[]> myDic = new Dictionary<int, int[]>();
        myDic.Add(0, new int[] { 2, 4, 6, 8, 10 });
        myDic.Add(1, new int[] { 4, 8, 16, 32, 64 });
        myDic.Add(2, new int[] { 2, 4, 8, 16, 32 });
        myDic.Add(3, new int[] { 8, 6, 4, 2, 0 });
        myDic.Add(4, new int[] { 1, 1, 1, 1, 1 });

        int[] keys = { 0, 1, 2, 3, 4 };
        int givenRan = UnityEngine.Random.Range(0, keys.Length);
        CmdSetGivenAndAnswer(keys[givenRan], given[keys[givenRan]]);
        string tv = given[keys[givenRan]];

        GameObject[][] platforms = { loopFirst, loopSecond, loopThird, loopFourth, loopFifth };
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < platforms[i].Length; y++)
            {
                int[] correct = myDic[correctAnswer];
                platforms[i][y].GetComponent<S4LanPlatformScript>().CmdChangeCorrectAnswer(correct[i]);
                //CmdUpdatePlatform(platforms[i][y], correct[i]);
            }
        }
        //RpcSetTv(tv);
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdatePlatform(GameObject platform, int correct)
    {
        if (isServer)
        {
            RpcUpdatePlatform(platform, correct);
        }
    }

    [ClientRpc]
    public void RpcUpdatePlatform(GameObject platform, int correct)
    {
        Debug.Log("Im in");
        platform.GetComponent<S4LanPlatformScript>().CmdChangeCorrectAnswer(correct);
        platform.GetComponent<S4LanPlatformScript>().CmdChangeValueGeneration();
    }

    [Command(requiresAuthority = false)]
    public void CmdSetGivenAndAnswer(int cor, string given)
    {
        if (isServer)
        {
            correctAnswer = cor;
            GivenStr = given;
        }
    }

    [ClientRpc(includeOwner = false)]
    public void RpcSetTv(string strTv)
    {
        TVText.GetComponent<TextMeshPro>().text = strTv;
    }




    [ClientRpc]
    public void RpcDefaultUI(bool status)
    {
        DefaultUi(status);
    }

    public void DefaultUi(bool status)
    {
        ReadyTimerGo.SetActive(!status);
        GivenGo.SetActive(status);
        //PathGo.SetActive(status);
    }

    public void GetAllUi()
    {
        S4GoStoreScript Lan4SceneStore = GameObject.Find("S4GoStorage").GetComponent<S4GoStoreScript>();
        ReadyTimerGo = Lan4SceneStore.ReadyTimerGo;
        GivenGo = Lan4SceneStore.GivenGo;
        PathGo = Lan4SceneStore.PathGo;
        Answers = Lan4SceneStore.Answers;
        TVText = Lan4SceneStore.TVText;

        loopFirst = Lan4SceneStore.loopFirst;
        loopSecond = Lan4SceneStore.loopSecond;
        loopThird = Lan4SceneStore.loopThird;
        loopFourth = Lan4SceneStore.loopFourth;
        loopFifth = Lan4SceneStore.loopFifth;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetWinner(bool status)
    {
        Debug.Log("Executed winner!");
        isWinnerBool = status;
    }

    public void isWinnerHook(bool oldValue, bool newValue)
    {
        if (isWinnerBool)
        {
            Debug.Log("Putangina");
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

    public void ReadyTimerIntHook(int oldValue, int newValue)
    {
        //ReadyTimerGo.GetComponent<TextMeshPro>().text = newValue.ToString();
    }

    public void GivenStrHook(string oldValue, string newValue)
    {
        GivenGo.GetComponent<TextMeshProUGUI>().text = GivenStr;
        TVText.GetComponent<TextMeshPro>().text = GivenStr;
        GameObject.Find("S4GoStorage").GetComponent<S4GoStoreScript>().correctAnswer = correctAnswer;
    }


    public void correctAnswerHook(int oldValue, int newValue)
    {
        if (isServer)
        {
            /*int correctPosition = UnityEngine.Random.Range(0, Answers.Length);
            int[] val = { 0,0,0};
            for(int i = 0; i < val.Length; i++)
            {
                if(correctPosition == i)
                {
                    val[i] = correctAnswer;
                }
                else
                {
                    val[i] = correctAnswer + UnityEngine.Random.Range(1, 10);
                }
            }
            RpcSetAnswers(val);*/
        }
    }

    [ClientRpc]
    public void RpcSetAnswers(int[] val)
    {
        /*for (int i = 0; i < Answers.Length; i++)
        {
            Answers[i].GetComponent<TextMeshPro>().text = val[i].ToString();
        }*/
    }
}
