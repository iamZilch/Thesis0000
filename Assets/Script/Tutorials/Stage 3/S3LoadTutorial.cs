using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class S3LoadTutorial : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startTrigger;
    [SerializeField] int stageNumber;
    [SerializeField] int check;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            loadConversation(check);
    }

    private void Start()
    {
        if (!GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[stageNumber])
        {
            if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber] > 0)
                loadConversation(GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber]);

            else
                startTrigger.SetActive(true);
        }

        else
        {
            if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber] > 0)
                loadConversation(GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber]);

            else
                startTrigger.SetActive(true);
        }
    }

    public void loadConversation(int choice)
    {
        // player.SetActive(true);
        switch (choice)
        {
            case 1: DialogueManager.StartConversation("Welcome to deslide", null, null, 0); break; // start
            case 2: DialogueManager.StartConversation("Logical Operators", null, null, 0); break;
            case 3: DialogueManager.StartConversation("Decision Statements", null, null, 0); break;
            case 4: DialogueManager.StartConversation("Else Statements", null, null, 0); break;
            case 5: DialogueManager.StartConversation("Else if Statements", null, null, 0); break;
            case 6: DialogueManager.StartConversation("Game Mechanics", null, null, 0); break;
            case 7: DialogueManager.StartConversation("Game Interactive Quest", null, null, 0); break;
            case 8: DialogueManager.StartConversation("Relational Operators", null, null, 0); break;
        }
        Debug.Log($"Checkpoint : {choice}");
    }
}


// "Welcome to deslide"
// "Logical Operators"
// "Decision Statements"
// "Else Statements"
// "Else if Statements"
// "Game Mechanics"
// "Game Interactive Quest"
// "Relational Operators"
