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
    public Vector3 originalPos;

    private void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        if (isServer)
        {
            Value.GetComponent<TextMeshPro>().text = "4";
            StartCoroutine(changeColor(int.Parse(Value.GetComponent<TextMeshPro>().text)));
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //&& (collision.gameObject.GetComponent<S4PlayerData>().currentPlatform != gameObject)
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if (!collision.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                return;
            }
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
        if (!collision.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            return;
        }

        S4PlayerData playerData = collision.gameObject.GetComponent<PlayerLanExtension>().S4LanPlayerData;
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix"))
        {
            CmdChangeSomeone(false);
            playerData.stepped = false;
            CmdStartCor(int.Parse(Value.GetComponent<TextMeshPro>().text));
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdStartCor(int curValue)
    {
        if (isServer)
        {
            RpcStartCor(curValue);
        }
    }

    [ClientRpc]
    public void RpcStartCor(int curValue)
    {
        if (isServer)
        {
            StartCoroutine(changeColor(curValue));
        }
    }

    IEnumerator changeColor(int curValue)
    {
        CmdChangeTimer(curValue.ToString());
        while (!someone)
        {
            int ti = int.Parse(Value.GetComponent<TextMeshPro>().text);
            if (ti <= 1)
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
        gameObject.transform.position = new Vector3(999f, 9999f, 9999f);
        if(staus == false)
        {
            ResetPlatform();
        }

        if (staus == true)
        {
            Debug.Log("RESETTING POS");
            gameObject.transform.position = originalPos;
            StopAllCoroutines();
            Start();
        }
    }

    public void ResetPlatform()
    {
        if (isServer)
        {
            StartCoroutine(ResetPlatformNumerator());
        }
    }

    IEnumerator ResetPlatformNumerator()
    {
        Debug.Log("Executed!");
        yield return new WaitForSeconds(Random.Range(5f, 11f));
        CmdSetActivePlatform(true);
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
