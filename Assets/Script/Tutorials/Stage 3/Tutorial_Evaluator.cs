using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_Evaluator : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] GameObject[] TextEval;
    public int Stage = 1;

    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeValue();
    }

    public void ChangeValue()
    {
        timer = Random.Range(5, 10) + 1;
        for (int i = 0; i < TextEval.Length; i++)
        {
            if (TextEval[i].GetComponent<TextMeshPro>().text.Equals("F"))
            {
                TextEval[i].GetComponent<TextHolderScript>().setValue("T");
            }
            else
            {
                TextEval[i].GetComponent<TextHolderScript>().setValue("F");
            }
            runDefault(TextEval[i]);
        }
        StartCoroutine(ChangeValueTimer());
    }

    IEnumerator ChangeValueTimer()
    {
        while (timer > 0)
        {
            if (timer == 1)
            {
                StopAllCoroutines();
                ChangeValue();
            }
            timer--;
            yield return new WaitForSeconds(1f);
        }
    }

    public void runDefault(GameObject toModify)
    {
        switch (Stage)
        {
            case 1:
                GameObject go = toModify.GetComponent<TextHolderScript>().GetStageGameObject();
                go.GetComponent<Tutorial_S1_Collider>().resetDefault();
                break;
            case 2:
                GameObject go1 = toModify.GetComponent<TextHolderScript>().GetStageGameObject();
                go1.GetComponent<Tutorial_S2_Collider>().resetDefault();
                break;
            case 3:
                GameObject go2 = toModify.GetComponent<TextHolderScript>().GetStageGameObject();
                go2.GetComponent<Tutorial_S3_Collider>().resetDefault();
                break;
            case 4:
                GameObject go3 = toModify.GetComponent<TextHolderScript>().GetStageGameObject();
                go3.GetComponent<Tutorial_S4_Collider>().resetDefault();

                break;
        }
    }
}
