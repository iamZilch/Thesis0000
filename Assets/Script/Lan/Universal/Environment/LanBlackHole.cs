using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class LanBlackHole : NetworkBehaviour
{
    [SerializeField] GameObject deadSpawnPoint;
    //-5.79
    private void OnTriggerStay(Collider collision)
    {
        LanStage1Handler LanStage1 = GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>();
        CmdCheckCheckStarting(LanStage1.IsGameStarting);
        if (LanStage1.IsGameStarting)
        {
            CmdCheckCollision(collision.gameObject.tag);
            if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch") && collision.gameObject.GetComponent<PlayerLanExtension>().isAlive)
            {
                PlayerLanExtension ple = collision.gameObject.GetComponent<PlayerLanExtension>();
                ple.isAlive = false;
                ple.CmdSendImDead();
                //Display disconnection
                ple.SpectatePlayer();
                LanStage1.CmdDecAlivePlayer(ple.isAlive);
                CmdCheckFall();
            }
            collision.gameObject.transform.position = deadSpawnPoint.transform.position;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -10f, gameObject.transform.position.z);
            Invoke(nameof(resetPos), 2f);
        }
    }

    public void resetPos()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -5.79f, gameObject.transform.position.z);
    }

   

    public void ResetTransform()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -5.79f, gameObject.transform.position.z);
    }

    [Command(requiresAuthority = false)]
    void CmdCheckFall()
    {
        Debug.Log("We have a hit");
    }

    [Command(requiresAuthority = false)]
    void CmdCheckCheckStarting(bool status)
    {
        Debug.Log($"Game is starting : {status}");
    }

    [Command(requiresAuthority = false)]
    void CmdCheckCollision(string name)
    {
        Debug.Log($"Collided to {name}");
    }


}
