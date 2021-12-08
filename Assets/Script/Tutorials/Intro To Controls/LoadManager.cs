using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] GameObject startTrigger;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            loadConversation(0);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            loadConversation(1);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            loadConversation(2);
    }

    private void Start()
    {
        if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[0] != 0)
            loadConversation(GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[0]);

        else
            startTrigger.SetActive(true);

    }

    public void loadConversation(int choice)
    {
        player.SetActive(true);
        switch (choice)
        {
            case 0: DialogueManager.StartConversation("Movement Tutorials", null, null, 0); break; // start
            case 1: DialogueManager.StartConversation("Movement Tutorials", null, null, 9); break; // after green platform
            case 2: DialogueManager.StartConversation("Movement Tutorials", null, null, 10); break; // after red platform
            case 3: DialogueManager.StartConversation("Movement Tutorials", null, null, 13); break; // after collecting speed up
            case 4: DialogueManager.StartConversation("Movement Tutorials", null, null, 15); break; // after pink platform
            case 5: DialogueManager.StartConversation("Movement Tutorials", null, null, 18); break; // after pressing 1st skill
            case 6: DialogueManager.StartConversation("Movement Tutorials", null, null, 28); break; // after collecting coool!
        }
    }
}
