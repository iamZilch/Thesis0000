using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_S1_Collider : MonoBehaviour
{
        [Header("Text Holder Game Object")]
        [SerializeField] GameObject TextHolderGameObject;

        [Header("Main Handler")]
        [SerializeField] GameObject Main;

        [Header("Environment Dependency")]
        [SerializeField] GameObject Platform;
        [SerializeField] GameObject GameObjectCollider;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
            {
                GameObject Stage3Main = GameObject.Find("TutorialStage3MainHandler");
                if (!Stage3Main.GetComponent<Tutorial_Stage3_MainHandler>().correctAnswer.Equals(TextHolderGameObject.GetComponent<TextHolderScript>().startValue))
                {
                    GameObjectCollider.SetActive(true);
                    Platform.SetActive(false);
                    Invoke(nameof(resetDefault), Random.Range(5, 10));
                }
                else
                {
                    //If correct
                    Main.GetComponent<Tutorial_Stage3_MainHandler>().GenerateGiven();
                    //GameObject.Find("SoundManager").GetComponent<S3SoundManager>().PlayMusic(0);
                }
            }
        }

        public void resetDefault()
        {
            GameObjectCollider.SetActive(false);
            Platform.SetActive(true);
        }
    
}
