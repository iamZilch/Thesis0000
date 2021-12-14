using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI firstSkillBtnText;
    [SerializeField] Button firstSkillButton;

    [SerializeField] TextMeshProUGUI ultiSkillBtnText;
    [SerializeField] Button ultiSkillButton;
    [SerializeField] GameObject player;
    void Start()
    {
        player.GetComponent<SkillControls>().loadButtons(firstSkillButton, ultiSkillButton, firstSkillBtnText, ultiSkillBtnText);
    }

    public void QuitGame()
    {
        GameObject.Find("Opening_Game_Script").SetActive(false);
        SceneManager.LoadScene("Opening_Game_Scene");
    }
}
