using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanS5Object : NetworkBehaviour
{
    public int value = 0;
    public float cooldown = 8f;

    void Start()
    {
        if (isServer)
        {
            InitColor();
            StartCoroutine(ChangeColor());
        }
    }

    public void InitColor()
    {
        if (!isServer)
        {
            return;
        }
        Color[] colors = { Color.green, Color.red, Color.blue };
        string[] colorsStr = { "green", "red", "blue" };
        int rand = Random.Range(0, colors.Length);
        switch (rand)
        {
            case 0:
                CmdChangeValue(1);
                break;
            case 1:
                CmdChangeValue(2);
                break;
            case 2:
                CmdChangeValue(4);
                break;
        }
        CmdChangeColor(rand);
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            if (cooldown <= 0)
            {
                InitColor();
                cooldown = Random.Range(8f, 13f);
            }
            cooldown--;
            yield return new WaitForSeconds(1f);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeColor(int rand)
    {
        RpcChangeColor(rand);
    }

    [ClientRpc]
    public void RpcChangeColor(int rand)
    {
        Color[] colors = { Color.green, Color.red, Color.blue };
        string[] colorsStr = { "green", "red", "blue" };
        var goRenderer = GetComponent<Renderer>();
        goRenderer.material.SetColor("_Color", colors[rand]);
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeValue(int val)
    {
        RpcChangeValue(val);
    }

    [ClientRpc]
    public void RpcChangeValue(int val)
    {
        value = val;
    }
}
