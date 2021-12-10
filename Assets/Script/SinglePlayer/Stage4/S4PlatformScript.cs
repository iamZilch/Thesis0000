using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4PlatformScript : MonoBehaviour
{
    [SerializeField] public GameObject Value;

    public string colorValue = "";
    public bool someone = false;

    private void Start()
    {
        Value.GetComponent<TextMeshPro>().text = "4";
        StartCoroutine(changeColor());
    }
    

    private void OnTriggerStay(Collider collision)
    {
        //&& (collision.gameObject.GetComponent<S4PlayerData>().currentPlatform != gameObject)
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if ((collision.gameObject.GetComponent<S4PlayerData>().colorToStep.Equals(colorValue)))
            {
                collision.gameObject.GetComponent<S4PlayerData>().MasterPlayerData(gameObject);
                collision.gameObject.GetComponent<S4PlayerData>().stepped = true;
            }
            else
            {
                if (!collision.gameObject.GetComponent<S4PlayerData>().stepped)
                {
                    gameObject.SetActive(false);
                }
            }
            someone = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix"))
        {
            someone = false;
            collision.gameObject.GetComponent<S4PlayerData>().stepped = false;
            StartCoroutine(changeColor());
        }
    }

    
    IEnumerator changeColor()
    {
        while (!someone)
        {
            if (Value.GetComponent<TextMeshPro>().text.Equals("1"))
            {
                Value.GetComponent<TextMeshPro>().text = "4";
                Color[] colors = { Color.green, Color.red, Color.blue};
                string[] colorsStr = { "green", "red", "blue"};
                int rand = Random.Range(0, colors.Length);
                colorValue = colorsStr[rand];
                var goRenderer = GetComponent<Renderer>();
                goRenderer.material.SetColor("_Color", colors[rand]);
            }
            int co = int.Parse(Value.GetComponent<TextMeshPro>().text);
            co--;
            Value.GetComponent<TextMeshPro>().text = co.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    
}
