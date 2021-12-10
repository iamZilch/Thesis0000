using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4PlayerData : MonoBehaviour
{
    [SerializeField] GameObject PathGo;

    string[] colorsStr = { "green", "red", "blue" };

    public int countToStep = 0;
    public string colorToStep = "";
    public bool stepped = false;

    public GameObject currentPlatform = null;

    private void Start()
    {
        GenerateToSteps();
    }

    public void GenerateToSteps()
    {
        countToStep = Random.Range(1, 4);
        colorToStep = colorsStr[Random.Range(0, colorsStr.Length)];
        PathGo.GetComponent<TextMeshProUGUI>().text = $"Color: {colorToStep} - {countToStep}x";
    }

    public void GetCurrentPlatform(GameObject platform)
    {
        currentPlatform = platform;
    }

    public bool isCurrentPlatform(GameObject platform)
    {
        return ReferenceEquals(currentPlatform, platform);
    }
    
    public void MasterPlayerData(GameObject platform)
    {
        if (!isCurrentPlatform(platform))
        {
            currentPlatform = platform;
            countToStep--;
            PathGo.GetComponent<TextMeshProUGUI>().text = $"Color: {colorToStep} - {countToStep}x";
            if (countToStep == 0)
            {
                GenerateToSteps();
            }
        }
    }
}
