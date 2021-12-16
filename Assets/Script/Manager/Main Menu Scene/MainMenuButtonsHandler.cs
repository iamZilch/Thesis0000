using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] tutorialButtons;
    [SerializeField] GameObject[] challengeButtons;

    public void tutorialButtonsEnabler() // only uncomment in build
    {
        // tutorialButtons[0].GetComponent<Button>().interactable = true;

        // for (int x = 1; x < tutorialButtons.Length; x++)
        //     tutorialButtons[x].GetComponent<Button>().interactable = GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[x];
    }

    public void challengeButtonsEnabler() // only uncomment in build
    {
        // for (int x = 0; x < challengeButtons.Length; x++)
        //     challengeButtons[x].GetComponent<Button>().interactable = GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[x + 1];
    }


}
