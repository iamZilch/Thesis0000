using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class Stage4S3Lan : NetworkBehaviour
{
    [SerializeField] public GameObject Text;
    [SerializeField] public GameObject triggerGo;
    [SerializeField] public GameObject Down;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix"))
        {
            LanStage3Handler handler = GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>();
            string textStr = Text.GetComponent<TextMeshPro>().text;
            if (textStr.Equals(handler.correctAnswer))
            {
                handler.UpdatePlayerCorrect();
                CmdDisableDown(false);
                handler.CmdChangeGiven();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(ResetDefaults());
    }

    [Command(requiresAuthority = false)]
    public void CmdDisableDown(bool status)
    {
        RpcDisableDown(status);
    }

    [ClientRpc]
    public void RpcDisableDown(bool status) //TO SET TO DEFAULT MAKE IT TRUE
    {
        Down.SetActive(status);
    }

    IEnumerator ResetDefaults()
    {
        yield return new WaitForSeconds(2f);
        CmdDisableDown(true);
        StopAllCoroutines();
    }
}
