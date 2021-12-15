using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class LanS5PickupHandler : NetworkBehaviour
{
    [SerializeField] public GameObject[] buttons;

    public int[] buttonsValue = { 0, 0, 0, 0, 0};
    public int selectedButton = 0;
    public bool isSubmitting = false;
    public bool canPick = false;
    public int index = 0;
    public bool init = false;

    public void InvokeButton(int buttonIndex)
    {
        if (!isSubmitting)
        {
            CmdSelectedButton(buttonIndex);
            CmdCanPick(true);
        }
        if(isSubmitting)
        {
            if(buttonIndex == GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().correctKeyList[index])
            {
                buttons[buttonIndex].GetComponent<Image>().color = Color.green;
                index++;
            }
            else if(buttonIndex != GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().correctKeyList[index])
            {
                buttons[buttonIndex].GetComponent<Image>().color = Color.red;
                index++;
            }
            CmdSelectedButton(buttonIndex);
        }
        CheckAnswer();
    }

    public void ExitingSubmit()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
        index = 0;
        init = false;
    }

    public void EnterSubmit()
    {
        if (!init)
        {
            init = true;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void CheckAnswer()
    {
        int correct = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<Image>().color == Color.green)
            {
                correct++;
            }
        }
        if (correct == 5)
        {
            //THE GAME IS DONE!
            GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().DeclareWinner();
            Debug.Log("Winner");
        }
    }

    #region BUTTON UI
    public void UpdateButtonUi()
    {
        buttons[selectedButton].GetComponentInChildren<TextMeshProUGUI>().text = buttonsValue[selectedButton].ToString();
    }

    #endregion

    public void ClearButton()
    {
        CmdCanPick(false);
        SetValue();
    }

    public void SetValue()
    {
        buttonsValue[selectedButton] = 0;
        UpdateButtonUi();
    }

 //   [Command(requiresAuthority =false)]
    public void CmdButtonValue(int value)
    {
        RpcButtonValue(value);
    }

//    [ClientRpc(includeOwner = false)]
    public void RpcButtonValue(int value)
    {
        buttonsValue[selectedButton] += value;
        UpdateButtonUi();
    }

 //   [Command(requiresAuthority = false)]
    public void CmdSelectedButton(int buttonIndex)
    {
        RpcSelectedButton(buttonIndex);
    }

   // [ClientRpc(includeOwner = false)]
    public void RpcSelectedButton(int buttonIndex)
    {
        selectedButton = buttonIndex;
    }

//    [Command(requiresAuthority = false)]
    public void CmdCanPick(bool status)
    {
        RpcCanPick(status);
    }

 //   [ClientRpc(includeOwner = false)]
    public void RpcCanPick(bool status)
    {
        canPick = status;
    }
}
