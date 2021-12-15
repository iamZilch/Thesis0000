using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Lan5MiddleCollider : NetworkBehaviour
{
    [SerializeField] public GameObject wrong;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            if (other.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                LanStage5Handler s5h = GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>();
                LanS5PickupHandler pickH = s5h.pickUP.GetComponent<LanS5PickupHandler>();
                bool correct = true;
                for (int i = 0; i < pickH.buttons.Length; i++)
                {
                    Debug.Log($"pickH Value : {pickH.buttonsValue[i]} ::: Correct: {s5h.correctArrangement[s5h.correctKeyList[i]]} :::: EVAL : {pickH.buttonsValue[i] != s5h.correctArrangement[s5h.correctKeyList[i]]}");
                    if (pickH.buttonsValue[i] != s5h.correctArrangement[s5h.correctKeyList[i]])
                    {
                        correct = false;
                        break;
                    }
                }
                if (correct)
                {
                    wrong.SetActive(false);
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wrong.SetActive(true);
    }
}
