using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4PlatformScript : MonoBehaviour
{
    [SerializeField] public GameObject Value;

    public string intValue = "";
    public bool isCorrect = false;
    public int correctAnswer = 0;
    public bool isStep = false;

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
            if (isCorrect)
            {
                isStep = true;
            }
        }
    }


    IEnumerator respawnPlatform()
    {
        yield return new WaitForSeconds(5f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1000, gameObject.transform.position.z);
    }

    public void StartGeneration()
    {
        int chance = Random.Range(0, 5);
        if(chance == 0)
        {
            isCorrect = true;
            updateUi(correctAnswer);
        }
        else
        {
            isCorrect = false;
            updateUi(correctAnswer + Random.Range(1, 10));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix")))
        {
            isStep = false;
            StartCor();
        }
    }

    public void StartCor()
    {
        StartCoroutine(changeMe());
    }

    IEnumerator changeMe()
    {
        while (!isStep)
        {
            StartGeneration();
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    public void updateUi(int value)
    {
        Value.GetComponent<TextMeshPro>().text = value.ToString();
    }




}
