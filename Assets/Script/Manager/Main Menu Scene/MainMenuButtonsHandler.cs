using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] tutorialButtons;
    [SerializeField] GameObject[] challengeButtons;
    [SerializeField] GameObject QuizModeButton;
    [SerializeField] GameObject MultiplayerButton;

    public void tutorialButtonsEnabler() // only uncomment in build
    {
        tutorialButtons[0].GetComponent<Button>().interactable = true;

        for (int x = 1; x < tutorialButtons.Length; x++)
            tutorialButtons[x].GetComponent<Button>().interactable = GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[x];
    }

    public void challengeButtonsEnabler() // only uncomment in build
    {
        for (int x = 0; x < challengeButtons.Length; x++)
            challengeButtons[x].GetComponent<Button>().interactable = GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[x + 1];
    }

    private void Start()
    {
        quizAndmulti();
    }

    public void quizAndmulti() // only uncomment in build
    {
        if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerLevel >= 20)
            QuizModeButton.GetComponent<Button>().interactable = true;
        else
            QuizModeButton.GetComponent<Button>().interactable = false;

        if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[5])
            MultiplayerButton.GetComponent<Button>().interactable = true;
        else
            MultiplayerButton.GetComponent<Button>().interactable = false;
    }


}
