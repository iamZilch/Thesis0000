using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Stage2S3Lan : NetworkBehaviour
{
    [SerializeField] public GameObject Text;
    [SerializeField] public GameObject trigger;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Maze"))
        {
            LanStage3Handler handler = GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>();
            string strText = Text.GetComponent<TextMeshPro>().text;
            if (strText.Equals(handler.correctAnswer))
            {
                handler.UpdatePlayerCorrect();
                CmdSetTrigger(false);
                handler.CmdChangeGiven();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.6f);
        CmdSetTrigger(true);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetTrigger(bool status)
    {
        RpcSetTrigger(status);
    }

    [ClientRpc]
    public void RpcSetTrigger(bool status) //TRUE FOR DEFAULT
    {
        trigger.SetActive(status);
    }
}
