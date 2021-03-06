using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PowerUpScript : MonoBehaviour
{
    AdvancedWalkerController adv;
    Animator anim;
    float normSpeed;
    float speed;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Maze") || other.tag.Equals("Trix") || other.tag.Equals("Zilch"))
        {
            if (gameObject.tag.Equals("SpeedUp"))
            {
                adv = other.GetComponent<AdvancedWalkerController>();
                anim = other.GetComponent<Animator>();

                gameObject.transform.position = new Vector3(0, gameObject.transform.position.y - 100f, 0);
                Debug.Log(anim.speed);
                normSpeed = anim.speed;
                speed = adv.getMovementSpeed();
                adv.setMovementSpeed(10f);
                anim.speed = 2.5f;
                StartCoroutine(timeLimit());
            }

            else if (gameObject.tag.Equals("CDReset"))
            {
                other.GetComponent<SkillControls>().resetCD();
                gameObject.SetActive(false);
            }

            else if (gameObject.tag.Equals("UltiPoint"))
            {
                other.GetComponent<SkillControls>().setUltiPoint(true);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator timeLimit()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log(adv.movementSpeed);
        Debug.Log(anim.speed);
        adv.setMovementSpeed(7f);
        adv.setJumpSpeed(6f);
        anim.speed = 1f;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
