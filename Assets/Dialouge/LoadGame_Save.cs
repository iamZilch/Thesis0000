using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;



public class LoadGame_Save : MonoBehaviour
{
    [SerializeField]GameObject player;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DialogueManager.StartConversation("Movement Tutorials", null, null,13);
            player.SetActive(true);

        }
    }
}
