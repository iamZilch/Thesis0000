using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCollider : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasCollided = false;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Trix")
        {
            if (!hasCollided)
            {
                GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[0] = 0;
                SaveData.SaveDataProgress(Database.instance);
            }

            hasCollided = true;

            Debug.Log("Its Colliding!");
        }
    }
}
