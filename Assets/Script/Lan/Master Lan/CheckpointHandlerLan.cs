using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandlerLan : MonoBehaviour
{
    [SerializeField] public GameObject posHandler;
    [SerializeField] public GameObject checkPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            if (other.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                posHandler.GetComponent<PositionHandler>().CheckPointGo = checkPoint;
            }
        }
    }
}
