using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class ButtonsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public SkillControls cast;
    public GameObject touchField;
    public bool canThrow = true;

    public void LanCastFirstSkill()
    {
        if (canThrow)
        {
            canThrow = false;
            player.GetComponent<LanThrower>().Fire();
            StartCoroutine(LanCastFirstSkilLCd());
        }
    }

    IEnumerator LanCastFirstSkilLCd()
    {
        yield return new WaitForSeconds(5f);
        canThrow = true;
    }

    public void LanCastUltimate()
    {
        player.GetComponent<PlayerLanExtension>().LanCastUltimate();
    }

    private void Start()
    {
        cast = player.GetComponent<SkillControls>();
    }

    public void setPlayer(GameObject ret)
    {
        player = ret;
        cast = player.GetComponent<SkillControls>();
    }

    public void castSkill()
    {
        cast.castSkill();
    }

    public void castUltimate()
    {
        cast.castUltimate();
    }

    public void jump()
    {
        player.GetComponent<CharacterKeyboardInput>().jumpButton();
    }

    public void pickUp()
    {
        player.GetComponent<Arithmetic_Character_Script>().pickUpMethod();
    }

}
