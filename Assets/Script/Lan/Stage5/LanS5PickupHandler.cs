using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LanS5PickupHandler : NetworkBehaviour
{
    [SerializeField] public GameObject[] buttons;

    public int[] buttonsValue = { 0, 0, 0, 0, 0};
    public int selectedButton = 0;
    public bool isSubmitting = false;
    public bool canPick = false;

    public void InvokeButton(int buttonIndex)
    {
        if (!isSubmitting)
        {
            CmdSelectedButton(buttonIndex);
            CmdCanPick(true);
        }
        if(isSubmitting)
        {
            CmdSelectedButton(buttonIndex);

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
        CmdButtonValue(0);
    }

    [Command(requiresAuthority =false)]
    public void CmdButtonValue(int value)
    {
        RpcButtonValue(value);
    }

    [TargetRpc]
    public void RpcButtonValue(int value)
    {
        buttonsValue[selectedButton] += value;
    }

    [Command(requiresAuthority = false)]
    public void CmdSelectedButton(int buttonIndex)
    {
        RpcSelectedButton(buttonIndex);
    }

    [TargetRpc]
    public void RpcSelectedButton(int buttonIndex)
    {
        selectedButton = buttonIndex;
    }

    [Command(requiresAuthority = false)]
    public void CmdCanPick(bool status)
    {
        RpcCanPick(status);
    }

    [TargetRpc]
    public void RpcCanPick(bool status)
    {
        canPick = status;
    }
}
