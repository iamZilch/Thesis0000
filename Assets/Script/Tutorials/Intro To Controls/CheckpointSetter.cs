using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    // Start is called before the first frame update

    bool hasCollided;
    [SerializeField] int checkpointNumber;

    private void Start()
    {
        hasCollided = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch"))
        {
            if (hasCollided.Equals(false))
            {
                GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[0] = checkpointNumber;
                SaveData.SaveDataProgress(Database.instance);
            }

            hasCollided = true;

            Debug.Log("Its Colliding!");
        }
    }
}
