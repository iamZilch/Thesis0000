using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class S2LoadTutorial : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startTrigger;
    [SerializeField] int stageNumber;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            loadConversation(4);
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
            case 1: DialogueManager.StartConversation("EXPLANATION OF OPERATORS", null, null, 6); break; // start
            case 2: DialogueManager.StartConversation("Division Explanation", null, null, 0); break;
            case 3: DialogueManager.StartConversation("Modulo Explanation", null, null, 0); break;
            case 4: DialogueManager.StartConversation("Modulo Explanation", null, null, 6); break;
            case 5: DialogueManager.StartConversation("Game Mechanics", null, null, 0); break;
        }
    }
}
