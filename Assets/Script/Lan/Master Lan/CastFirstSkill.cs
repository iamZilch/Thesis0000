using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CastFirstSkill : NetworkBehaviour
{
    [SerializeField] public GameObject Player;
    
    public void fire()
    {
        try
        {
            FireMe();
        }catch (Exception e) { }
    }

    [Command(requiresAuthority = false)]
    public void FireMe()
    {
        GetComponent<ButtonsHandler>().player.GetComponent<LanThrower>().Fire();
    }
}
