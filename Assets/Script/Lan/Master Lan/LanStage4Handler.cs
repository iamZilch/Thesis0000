using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class LanStage4Handler : NetworkBehaviour
{
    [SerializeField] GameObject ReadyTimerGo;
    [SerializeField] GameObject GivenGo;
    [SerializeField] GameObject PathGo;
    [SerializeField] GameObject[] Answers;
    [SerializeField] GameObject TVText;

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

    [Command(requiresAuthority =false)]
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
            { 10, "If n = 0\nand you repeat \nn = n + 2, 5 times" },
            { 16, "If n = 2\nand you repeat \nn = n * 2, 4 times." },
            { 8, "If n = 1\nand you repeat \nn = n + n, 3 times. " }
        };

        int[] keys = { 10, 16, 8 };
        int givenRan = Random.Range(0, keys.Length);
        CmdSetGivenAndAnswer(keys[givenRan], given[keys[givenRan]]);
        string tv = given[keys[givenRan]];
        //RpcSetTv(tv);
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
        PathGo.SetActive(status);
    }

    public void GetAllUi()
    {
        S4GoStoreScript Lan4SceneStore = GameObject.Find("S4GoStorage").GetComponent<S4GoStoreScript>();
        ReadyTimerGo = Lan4SceneStore.ReadyTimerGo;
        GivenGo = Lan4SceneStore.GivenGo;
        PathGo = Lan4SceneStore.PathGo;
        Answers = Lan4SceneStore.Answers;
        TVText = Lan4SceneStore.TVText;
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
                Debug.Log("I AM WINNER ! SHOW CONGRATS HERE!");
            }
            else
            {
                Debug.Log("I AM LOSER ! SHOW SHIT MSG HERE!");
            }
            SceneManager.LoadScene("Main_Menu_Scene");
        }
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
        int correct = Random.Range(0, Answers.Length);
        for(int i = 0; i < Answers.Length; i++)
        {
            if(correct == i)
            {
                Answers[i].GetComponent<TextMeshPro>().text = correctAnswer.ToString();
            }
            else
            {
                Answers[i].GetComponent<TextMeshPro>().text = (correctAnswer + Random.Range(1, 15)).ToString();
            }
        }
    }
}
