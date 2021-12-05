using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
