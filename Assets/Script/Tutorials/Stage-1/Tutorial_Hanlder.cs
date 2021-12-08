using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Hanlder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ButtonHandler;
    public bool buttonHandlerStatus = true;

    public GameObject stage1Handler;
    public int given;

    public GameObject MazeChar;
    public bool mazeStatus = true;

    public Camera myCmam;
    public bool camEnabled = true;

    private void OnEnable()
    {
        ButtonHandler.SetActive(buttonHandlerStatus);
        myCmam.enabled = camEnabled;
        MazeChar.SetActive(mazeStatus);

    }
}
