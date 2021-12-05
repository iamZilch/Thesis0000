using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EndGameLan : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Raze"))
        {
            LanStage3Handler handler = GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>();
            handler.flag = true;
            handler.CmdIsWinner(true);
        }
    }
}
