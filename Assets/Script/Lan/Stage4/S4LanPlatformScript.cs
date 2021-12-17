using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class S4LanPlatformScript : NetworkBehaviour
{
    [SerializeField] public GameObject Value;

    public bool someone = false;
    public string intValue = "";
    public bool isCorrect = false;
    public int correctAnswer = 0;
    public Vector3 originalPos;

    private void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        if (isServer)
        {
            CmdChangeValueGeneration();
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
            if (!isCorrect)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1000, gameObject.transform.position.z);
                //StartCoroutine(respawnPlatform());
            }
            if (isCorrect)
            {
                CmdChangeSomeone(true);
            }
            
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeValueGeneration()
    {
        if (isServer)
        {
            changingCor();
        }
    }

    IEnumerator changingCor()
    {
        while (true)
        {
            CmdStartGeneration();
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeCorrectAnswer(int ans)
    {
        if (isServer)
        {
            RpcChangeCorrectAnswer(ans);
        }
    }

    [ClientRpc]
    public void RpcChangeCorrectAnswer(int ans)
    {
        correctAnswer = ans;
    }

    [Command(requiresAuthority = false)]
    public void CmdStartGeneration()
    {
        if (isServer)
        {
            int chance = Random.Range(0, 5);
            if (chance == 0)
            {
                isCorrect = true;
                RpcChangePlatformProperty(isCorrect, correctAnswer);
            }
            else
            {
                isCorrect = false;
                RpcChangePlatformProperty(isCorrect, correctAnswer);
            }
        }
    }

    [ClientRpc]
    public void RpcChangePlatformProperty(bool status, int value)
    {
        isCorrect = status;
        Value.GetComponent<TextMeshPro>().text = value.ToString();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            return;
        }

        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix"))
        {
            CmdChangeSomeone(false);

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
