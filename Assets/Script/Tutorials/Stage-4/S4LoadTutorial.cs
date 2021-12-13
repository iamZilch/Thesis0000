using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class S4LoadTutorial : MonoBehaviour
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
        switch (choice)
        {
            case 1: DialogueManager.StartConversation("Starting Conversastion", null, null, 0); break; // start
            case 2: DialogueManager.StartConversation("Conditional Loops", null, null, 0); break;
            case 3: DialogueManager.StartConversation("Unconditional Loops", null, null, 0); break;
            case 4: DialogueManager.StartConversation("Game Mechanics", null, null, 0); break;
            case 5: DialogueManager.StartConversation("Question And Answer", null, null, 0); break;
        }
    }
}
