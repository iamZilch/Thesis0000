using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_S4_Collider : MonoBehaviour
{
    [Header("Text Holder Game Object")]
    [SerializeField] GameObject TextHolderGameObject;

    [Header("Main Handler")]
    [SerializeField] GameObject Main;

    [Header("Environment Dependency")]
    [SerializeField] GameObject Platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            GameObject Stage3Main = GameObject.Find("TutorialStage3MainHandler");
            if (Stage3Main.GetComponent<Tutorial_Stage3_MainHandler>().correctAnswer.Equals(TextHolderGameObject.GetComponent<TextHolderScript>().startValue))
            {
                Main.GetComponent<Tutorial_Stage3_MainHandler>().Congrats.SetActive(true);
                Main.GetComponent<Tutorial_Stage3_MainHandler>().SetUiActive(false);
                //GameObject.Find("SoundManager").GetComponent<S3SoundManager>().PlayMusic(0); 
                Platform.SetActive(false);


            }
        }
    }

    public void resetDefault()
    {
        Platform.SetActive(true);
    }
}
