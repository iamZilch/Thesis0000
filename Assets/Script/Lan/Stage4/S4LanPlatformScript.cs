using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class S4LanPlatformScript : NetworkBehaviour
{
    [SerializeField] public GameObject Value;

    public string colorValue = "";
    public bool someone = false;

    private void Start()
    {
        if (isServer)
        {
            Value.GetComponent<TextMeshPro>().text = "4";
            StartCoroutine(changeColor());
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //&& (collision.gameObject.GetComponent<S4PlayerData>().currentPlatform != gameObject)
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            S4PlayerData playerData = collision.gameObject.GetComponent<PlayerLanExtension>().S4LanPlayerData;
            if ((playerData.colorToStep.Equals(colorValue)))
            {
                playerData.MasterPlayerData(gameObject);
                playerData.stepped = true;
            }
            else
            {
                if (!playerData.stepped)
                {
                    CmdSetActivePlatform(false);
                }
            }
            CmdChangeSomeone(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        S4PlayerData playerData = collision.gameObject.GetComponent<PlayerLanExtension>().S4LanPlayerData;
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix"))
        {
            CmdChangeSomeone(false);
            playerData.stepped = false;
            StartCoroutine(changeColor());
        }
    }

    IEnumerator changeColor()
    {
        while (!someone)
        {
            if (Value.GetComponent<TextMeshPro>().text.Equals("1"))
            {
                CmdChangeTimer("4");
                CmdChangeColorValue();
            }
            int co = int.Parse(Value.GetComponent<TextMeshPro>().text);
            co--;
            CmdChangeTimer(co.ToString());
            yield return new WaitForSeconds(1f);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetActivePlatform(bool status)
    {
        if (isServer)
        {
            RpcSetActivePlatform(status);
        }
    }
    
    [ClientRpc(includeOwner = false)]
    public void RpcSetActivePlatform(bool staus)
    {
        gameObject.SetActive(staus);
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeTimer(string value)
    {
        if (isServer)
        {
            RpcChangeTimer(value);
        }
    }

    [ClientRpc(includeOwner = false)]
    public void RpcChangeTimer(string value)
    {
        Value.GetComponent<TextMeshPro>().text = value;
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeColorValue()
    {
        if (isServer)
        {
            Value.GetComponent<TextMeshPro>().text = "4";
            Color[] colors = { Color.green, Color.red, Color.blue };
            string[] colorsStr = { "green", "red", "blue" };
            int rand = Random.Range(0, colors.Length);
            colorValue = colorsStr[rand];
            RpcChangeColorValue(rand);
        }
    }

    [ClientRpc(includeOwner = false)]
    public void RpcChangeColorValue(int rand)
    {
        Color[] colors = { Color.green, Color.red, Color.blue };
        string[] colorsStr = { "green", "red", "blue" };
        ChangeColorValue(colorsStr[rand]);
        var goRenderer = GetComponent<Renderer>();
        goRenderer.material.SetColor("_Color", colors[rand]);
    }

    public void ChangeColorValue(string color)
    {
        colorValue = color;
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeSomeone(bool status)
    {
        RpcChangeSomeone(status);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcChangeSomeone(bool status)
    {
        someone = status;
    }
}
