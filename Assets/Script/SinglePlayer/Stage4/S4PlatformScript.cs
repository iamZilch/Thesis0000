using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4PlatformScript : MonoBehaviour
{
    [SerializeField] public GameObject Value;

    public string intValue = "";
    public bool isCorrect = false;

    private void Start()
    {
        
    }


    private void OnTriggerStay(Collider collision)
    {
        //&& (collision.gameObject.GetComponent<S4PlayerData>().currentPlatform != gameObject)
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if (!isCorrect)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1000, gameObject.transform.position.z);
                StartCoroutine(respawnPlatform());
            }
        }
    }

    IEnumerator respawnPlatform()
    {
        yield return new WaitForSeconds(5f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1000, gameObject.transform.position.z);
    }

    public void updateUi()
    {
        Value.GetComponent<TextMeshPro>().text = intValue;
    }




}
