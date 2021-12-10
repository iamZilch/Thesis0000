using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LanStage3Handler : NetworkBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject GivenGo;
    [SerializeField] public GameObject PlayerCorrectAnswer;
    [SerializeField] public GameObject ReadyTimerGo;

    [Header("Stage1")]
    [SerializeField] public GameObject S1Bool1;
    [SerializeField] public GameObject S1Bool2;
    [SerializeField] public GameObject S1Bool3;
    [SerializeField] public GameObject S1Bool4;

    [Header("Stage2")]
    [SerializeField] public GameObject S2Bool1;
    [SerializeField] public GameObject S2Bool2;
    [SerializeField] public GameObject S2Bool3;

    [Header("Stage3")]
    [SerializeField] public GameObject S3Bool1;
    [SerializeField] public GameObject S3Bool2;
    [SerializeField] public GameObject S3Bool3;
    [SerializeField] public GameObject S3Bool4;

    [Header("Stage4")]
    [SerializeField] public GameObject S4Bool1;
    [SerializeField] public GameObject S4Bool2;
    [SerializeField] public GameObject S4Bool3;
    [SerializeField] public GameObject S4Bool4;

    GameObject buttonHandler;

    public int playerCorrectAnswerInt = 0;
    public bool flag = false;

    [SyncVar(hook =nameof(rightBoolHook))]
    public bool rightBool = false;
    [SyncVar(hook = nameof(leftBoolHook))]
    public bool leftBool = true;
    [SyncVar(hook = nameof(GivenHook))]
    public string Given = "";
    [SyncVar(hook = nameof(ReadyCountDownHook))]
    public int ReadyCountDown = 4;
    [SyncVar]
    public string correctAnswer = "";
    [SyncVar(hook =nameof(isWinnerHook))]
    public bool isWinner = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(isServer && Input.GetKeyDown(KeyCode.Alpha7))
        {
            CmdChangeGiven();
        }
    }

    public void MasterStart()
    {
        if (isServer)
        {
            buttonHandler = GameObject.Find("ButtonHandler");
            buttonHandler.SetActive(false);
            ResetDefault();
            StartCoroutine(ReadTimerCoroutine());
        }
    }


    public void ResetDefault()
    {
        Debug.Log("RESET STAGE3 LAN MASTER START!");
        ReadyTimerGo.SetActive(false);
        PlayerCorrectAnswer.SetActive(false);
        GivenGo.SetActive(false);
        ReadyCountDown = 4;
        correctAnswer = "";
        Given = "";
    }

    IEnumerator ReadTimerCoroutine()
    {
        Debug.Log("Coroutine STAGE3 LAN MASTER START!");
        ReadyTimerGo.SetActive(true);
        yield return new WaitForSeconds(1f);
        if(ReadyCountDown == 0)
        {
            StopAllCoroutines();
            PlayerCorrectAnswer.SetActive(true);
            GivenGo.SetActive(true);
            ReadyTimerGo.SetActive(false);
            StartCoroutine(CmdChangeBoolNumerator());
            CmdChangeGiven();
            buttonHandler.SetActive(true);
            Debug.Log("Executed stage 3 coroutine!");
        }
        else
        {
            CmdDecTimer();
            StartCoroutine(ReadTimerCoroutine());
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdDecTimer()
    {
        ReadyCountDown--;
    }

    [Command(requiresAuthority = false)]
    public void CmdIsWinner(bool status)
    {
        isWinner = status;
    }


    [Command(requiresAuthority = false)]
    public void CmdChangeGivenTest()
    {
        correctAnswer = "F";
    }

    public void GetAllGui()
    {
        S3GoStoreScript Lan3SceneStore = GameObject.Find("S3GoStorage").GetComponent<S3GoStoreScript>();
        S1Bool1 = Lan3SceneStore.S1Bool1;
        S1Bool2 = Lan3SceneStore.S1Bool2;
        S1Bool3 = Lan3SceneStore.S1Bool3;
        S1Bool4 = Lan3SceneStore.S1Bool4;

        S2Bool1 = Lan3SceneStore.S2Bool1;
        S2Bool2 = Lan3SceneStore.S2Bool2;
        S2Bool3 = Lan3SceneStore.S2Bool3;

        S3Bool1 = Lan3SceneStore.S3Bool1;
        S3Bool2 = Lan3SceneStore.S3Bool2;
        S3Bool3 = Lan3SceneStore.S3Bool3;
        S3Bool4 = Lan3SceneStore.S3Bool4;

        S4Bool1 = Lan3SceneStore.S4Bool1;
        S4Bool2 = Lan3SceneStore.S4Bool2;
        S4Bool3 = Lan3SceneStore.S4Bool3;
        S4Bool4 = Lan3SceneStore.S4Bool4;

        ReadyTimerGo = Lan3SceneStore.ReadyTimerGo;
        PlayerCorrectAnswer = Lan3SceneStore.PlayerCorrectAnswer;
        GivenGo = Lan3SceneStore.GivenGo;

    }

    IEnumerator CmdChangeBoolNumerator()
    {
        while (true)
        {
            CmdChangeBool(0);
            CmdChangeBool(1);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeBool(int direction)
    {
        if (direction == 0)
        {
            if (leftBool == true)
            {
                leftBool = false;
            }
            else
            {
                leftBool = true;
            }
        }
        else if(direction == 1)
        {
            if (rightBool == true)
            {
                rightBool = false;
            }
            else
            {
                rightBool = true;
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdShowResult()
    {
        RpcShowResult();
    }

    [ClientRpc]
    public void RpcShowResult()
    {
        ShowResult();
    }

    public void ShowResult()
    {
        if (flag == true)
        {
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().CongratsGo.SetActive(true);
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().DisableCoroutine();
        }
        else
        {
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().ThankYouGo.SetActive(true);
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().DisableCoroutine();
        }
    }

    public void isWinnerHook(bool oldValue, bool newValue)
    {
        if(newValue == true)
        {
            CmdShowResult();
        }
    }

    public void UpdatePlayerCorrect()
    {
        playerCorrectAnswerInt++;
        PlayerCorrectAnswer.GetComponent<TextMeshProUGUI>().text = playerCorrectAnswerInt.ToString();
    }

    public void GivenHook(string oldValue, string newValue)
    {
        GivenGo.GetComponent<TextMeshProUGUI>().text = newValue;
    }

    public void ReadyCountDownHook(int oldValue, int newValue)
    {
        if(newValue == 0)
        {
            ReadyTimerGo.SetActive(false);
        }
        else
        {
            ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
        }
    }

    public void rightBoolHook(bool oldValue, bool newValue)
    {
        string value;
        if(newValue == false)
        {
            value = "F";
        }
        else
        {
            value = "T";
        }
        S1Bool1.GetComponent<TextMeshPro>().text = value;
        S1Bool3.GetComponent<TextMeshPro>().text = value;

        S2Bool1.GetComponent<TextMeshPro>().text = value;
        S2Bool3.GetComponent<TextMeshPro>().text = value;

        S3Bool2.GetComponent<TextMeshPro>().text = value;
        S3Bool4.GetComponent<TextMeshPro>().text = value;

        S4Bool1.GetComponent<TextMeshPro>().text = value;
        S4Bool3.GetComponent<TextMeshPro>().text = value;
    }

    public void leftBoolHook(bool oldValue, bool newValue)
    {
        string value;
        if (newValue == false)
        {
            value = "F";
        }
        else
        {
            value = "T";
        }
        S1Bool2.GetComponent<TextMeshPro>().text = value;
        S1Bool4.GetComponent<TextMeshPro>().text = value;

        S2Bool2.GetComponent<TextMeshPro>().text = value;

        S3Bool1.GetComponent<TextMeshPro>().text = value;
        S3Bool3.GetComponent<TextMeshPro>().text = value;

        S4Bool2.GetComponent<TextMeshPro>().text = value;
        S4Bool4.GetComponent<TextMeshPro>().text = value;
    }

    [Command(requiresAuthority =false)]
    public void CmdChangeGiven()
    {
        List<string> TrueGiven = new List<string>
        {
            "if(True && False)" +
            "\n\t print((False || True) &&  False)" +
            "\nelse" + //this
            "\n\t print(True || (False && False))",

            "if(False || True)" + //this
            "\n\t print(True && True)" +
            "\nelse" +
            "\n\t print(False && True)",

            "if(True || (True && False))" + //this
            "\n\t print((False && True) || (True || False))" +
            "\nelse" +
            "\n\t print(False || True)",

            "if(False && !True)" +
            "\n\t print(!False && True)" +
            "\nelse if(False || (True && False))" +
            "\n\t print(True && True)" +
            "\nelse" +
            "\n\t print(!False || !True)",

            "if(False && (!True || False))" +
            "\n\tprint(!True || !False)" +
            "\nelse if(True && (False || True))" + //this
            "\n\t print()" +
            "\nelse" +
            "\n\t print()",

            "if(False && True)" +
            "\n\t print(False && False)" +
            "\nelse if((True && True) || False)" + //this
            "\n\t print(!False)" +
            "\n else" +
            "\n\t print(!True && False)",

            "if(True || !False)" + //this
            "\n\t print(False || (True && !False))" +
            "\nelse" +
            "\n\t print(True && True)"

        };

        List<string>FalseGiven = new List<string>
        {
            "if(False && False)" +
            "\n\t print(True && True)" +
            "\n else" + //this
            "\n\t print(!True)",

            "if(False || !False)" + //this
            "\n\t print(False && (True || False))" +
            "\n else" +
            "\n\t print(True || (False || True))",

            "if(True && (False || !True))" +
            "\n\t print(False || False)" +
            "\n else" + //this
            "\n\t print(True && (True && !True))",

            "if(True && !(True || False))" +
            "\n\t print(True || False)" +
            "\n else if(!True || (True && False))" +
            "\n\t print(!False && False)" +
            "\n else" + //this
            "\n\t print(!True && !(True || !False))",

            "if(False || False)" +
            "\n\t print(True || !True)" +
            "\n else if(False || True)" + //this
            "\n\t print(!False && (True || !False))" +
            "\n else" +
            "\n\t print(!False)",

            "if(True && (!True || !False))" + //this
            "\n\t print(!True || (False && True))" +
            "\n else" +
            "\n\t print(!False || True)",

            "if(False || !True)" +
            "\n\t print(!False || True)" +
            "\n else if(True && (!False || True))" + //this
            "\n\t print(!True && !False)" +
            "\n else" +
            "\n\t print(!True)"
        };

        int questionNum = Random.Range(0, 2);
        Debug.Log($"Question Num : {questionNum}");
        if(questionNum == 1)
        {
            Given = TrueGiven[Random.Range(0, TrueGiven.Count)];
            correctAnswer = "T";
        }
        else
        {
            Given = FalseGiven[Random.Range(0, FalseGiven.Count)];
            correctAnswer = "F";
        }
    }

    
}
