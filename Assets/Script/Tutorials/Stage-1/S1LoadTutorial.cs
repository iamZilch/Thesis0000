using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class S1LoadTutorial : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startTrigger;
    // [SerializeField] GameObject ctrlTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            loadConversation(3);
    }

    private void Start()
    {
        if (!GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[1])
        {
            if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[1] > 0)
                loadConversation(GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[1]);

            else
                startTrigger.SetActive(true);
        }

        else
        {
            if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[1] > 0)
                loadConversation(GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[1]);

            else
                startTrigger.SetActive(true);
        }

    }

    public void loadConversation(int choice)
    {
        player.SetActive(true);
        // ctrlTrigger.SetActive(true);
        switch (choice)
        {
            case 1: DialogueManager.StartConversation("Game Mechanics", null, null, 0); break; // start
            case 2: DialogueManager.StartConversation("Integer Tutorial", null, null, 0); break;
            case 3: DialogueManager.StartConversation("Float Tutorial", null, null, 0); break;
            case 4: DialogueManager.StartConversation("Characters Tutorial", null, null, 0); break;
            case 5: DialogueManager.StartConversation("String Tutorial", null, null, 0); break;
            case 6: DialogueManager.StartConversation("Boolean Tutorial", null, null, 0); break;
            case 7: DialogueManager.StartConversation("Double Tutorial", null, null, 0); break;
        }
    }
}
