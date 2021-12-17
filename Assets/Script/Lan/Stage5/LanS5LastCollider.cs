using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanS5LastCollider : NetworkBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            if (other.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().EnterSubmit();
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().isSubmitting = true;
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().enableAllButton();
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().clearBtnSetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            if (other.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().ExitingSubmit();
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().isSubmitting = false;
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().SetEnableButton(-1);
                GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().pickUP.GetComponent<LanS5PickupHandler>().clearBtnSetActive(true);
            }
        }

    }
}
