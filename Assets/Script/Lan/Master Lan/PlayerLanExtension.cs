using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;
using System;

public class PlayerLanExtension : NetworkBehaviour
{
    [Header("Player Cam")]
    [SerializeField] public GameObject playerRootCam;
    [SerializeField] public GameObject cam;
    [SerializeField] public GameObject cameraControls;
    public List<GameObject> players;
    public bool isAlive = true;
    public bool isReady = false;

    [SerializeField] public GameObject ballSpointPoint;
    Skills skillFX;
    public GameObject[] particles;

    public GameObject defaultRootCam;
    public GameObject defaultCam;

    int SpectateIndex = 0;
    bool spectating = false;
    int spectatingAlive = 0;
    Animator anim;
    Mover mover;
    float animSpeed;
    float movementSpeed;

    [Header("Player Skills")]
    public bool canUlti = false;
    public bool canFirst = true;

    public S4PlayerData S4LanPlayerData;
    public GameObject Stage5Handler;

    private void Start()
    {
        if (isLocalPlayer)
        {
            if (isServer && isClient) 
            {
                Debug.Log("Is Server");
                CmdAddReadyPlayer();
                CmdAddPlayerTotal();
            }
            if (isClient && !isServer)
            {
                Debug.Log("Is Client");
                CmdAddPlayerTotal();
            }
            GameObject buttonHandler = GameObject.Find("ButtonHandler");
            buttonHandler.GetComponent<ButtonsHandler>().player = gameObject;
            buttonHandler.GetComponent<ButtonsHandler>().cast = gameObject.GetComponent<SkillControls>();
            cameraControls.GetComponent<TouchField>().touchField = buttonHandler.GetComponent<ButtonsHandler>().touchField.GetComponent<FixedTouchField>();
            playerRootCam.SetActive(true);
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            CmdAddToPlayerList();
            StageLocalPlayerReference();
            skillFX = gameObject.GetComponentInChildren<Skills>();
            particles = skillFX.particles;
            defaultRootCam = playerRootCam;
            defaultCam = cam;
            GameObject.Find("NetworkManager").GetComponent<MasterLanScript>().player = gameObject;
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().Player = gameObject;
        }
    }


    private void Update()
    {
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.Alpha9) && isServer)
        {
            GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().RpcSpawnTestPowerUps();
        }
    }


    void FindThrower()
    {
        GameObject.Find("NetworkStorage").GetComponent<LanThrower>().player = gameObject;
    }

    void StageLocalPlayerReference() // SEE WHICH STAGES ARE LOADED
    {
        if (GameObject.Find("PlayerPositionHandler"))
        {
            GameObject.Find("PlayerPositionHandler").GetComponent<PositionHandler>().myPlayer = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().Player = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllText();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllPlatforms();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().CmdToAddAlivePlayer();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllPowerUps();
        }
        else if (GameObject.Find("PlayerPositionHandler2"))
        {
            GameObject.Find("PlayerPositionHandler2").GetComponent<PositionHandler>().myPlayer = gameObject;
            GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().Player = gameObject;
            GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().GetAllUiGo();
            GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().GetAllArithmeticSpawn();
        }
        else if (GameObject.Find("PlayerPositionHandler3"))
        {
            GameObject.Find("PlayerPositionHandler3").GetComponent<PositionHandler>().myPlayer = gameObject;
            GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>().Player = gameObject;
            GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>().GetAllGui();
        }
        else if (GameObject.Find("PlayerPositionHandler4"))
        {
            GameObject.Find("PlayerPositionHandler4").GetComponent<PositionHandler>().myPlayer = gameObject;
            S4LanPlayerData = GameObject.Find("PlayerData").GetComponent<S4PlayerData>();
            GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().GetAllUi();
        }
        else if (GameObject.Find("PlayerPositionHandler5"))
        {
            GameObject.Find("PlayerPositionHandler5").GetComponent<PositionHandler>().myPlayer = gameObject;
            Stage5Handler = GameObject.Find("Stage5Handler");
        }
    }

    #region Spectator Cam Script
    public void SpectatePlayer()
    {
        if(SpectateIndex >= players.Count)
        {
            SpectateIndex = 0;
        }
        Debug.Log($"I AM THE SERVER ! I AM ALIVE? = {isAlive}");
        for (int i = SpectateIndex; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerLanExtension>().isAlive && !players[i].GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                SpectateIndex++;
                GameObject alivePlayer = players[i].gameObject;
                spectatingAlive = i;
                GetComponent<PlayerLanExtension>().playerRootCam.GetComponent<SmoothPosition>().target = alivePlayer.transform;
                GetComponent<PlayerLanExtension>().playerRootCam.GetComponent<SmoothRotation>().target = alivePlayer.transform;
                Debug.Log("Executed exchange camera");
                break;
            }
        }
        if (!spectating)
        {
            spectating = true;
            StartCoroutine(SpectateCor());
        }
    }

    public IEnumerator SpectateCor()
    {
        while (!isAlive)
        {
            if (!players[spectatingAlive].GetComponent<PlayerLanExtension>().isAlive)
            {
                SpectatePlayer();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion


    [Command]
    void CmdAddPlayerTotal()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerTotal++;
    }

    [Command]
    void CmdAddReadyPlayer()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerReady++;
    }

    [Command]
    public void CmdDecReadyPlayer()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerReady--;
    }

    [Command]
    public void CmdSetReadyLocal(bool status)
    {
        RpcSetReadyLocal(status);
    }

    [ClientRpc]
    public void RpcSetReadyLocal(bool status)
    {
        isReady = status;
    }


    #region Player Dead Handling
    [Command]
    public void CmdSendImDead()
    {
        RpcSendImDead();
    }

    [ClientRpc]
    void RpcSendImDead()
    {
        isAlive = false;
    }

    #endregion

    #region MANAGE ADDING PLAYER TO LOCAL PLAYER LIST
    [Command]
    void CmdAddToPlayerList()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Add(gameObject);
        RpcTesting(gameObject);
    }

    [Command]
    void CmdAddPlayerToLocal()
    {
        if (isServer)
        {
            for(int i = 0; i < GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Count; i++)
            {
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().players.Clear();
                for (int y = 0; y < GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Count; y++)
                {
                    GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().players.Add(GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[y]);
                }
            }
        }
    }

    [ClientRpc]
    void RpcTesting(GameObject go)
    {
        CmdDebugMe();
    }

    [Command]
    void CmdDebugMe()
    {
        if (isServer)
        {
            for(int i = 0; i < GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Count; i++)
            {
                GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().RpcDebugMe();
                for(int y = 0; y < GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Count; y++)
                {
                    GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[i].GetComponent<PlayerLanExtension>().RpcAddLocal(GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan[y]);
                }
            }
        }
    }

    [ClientRpc]
    void RpcDebugMe()
    {
        if (isLocalPlayer)
        {
            players.Clear();
        }
    }

    [ClientRpc]
    void RpcAddLocal(GameObject playerGo)
    {
        if (isLocalPlayer)
        {
            players.Add(playerGo);
        }
    }

    #endregion

    [Command]
    public void CmdReposition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }


    [ClientRpc]
    void RpcAddToPlayerList(GameObject player)
    {
        players.Add(player);
    }

    [ClientRpc]
    public void SetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }

    [Command]
    public void ResetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }

    #region ULTI AND FIRST SKILL
    [Command]
    public void CmdFirstStatus(bool status)
    {
        if (isServer)
        {
            RpcFirstStatus(status);
        }
    }

    [TargetRpc]
    public void RpcFirstStatus(bool status)
    {
        canFirst = status;
        StopCoroutine(nameof(FirstSKillCd));
        StartCoroutine(FirstSKillCd());
    }

    IEnumerator FirstSKillCd()
    {
        yield return new WaitForSeconds(10f);
        CmdFirstStatus(true);
    }

    [Command]
    public void CmdUltiStatus(bool status)
    {
        if (isServer)
        {
            RpcUltiStatus(status);
        }
    }

    [TargetRpc]
    public void RpcUltiStatus(bool status)
    {
        canUlti = status;
    }

    #endregion

    [Command]
    public void CmdSpeedUp()
    {
        if (isServer)
        {
            RpcSpeedUp();
        }
    }

    [TargetRpc]
    public void RpcSpeedUp()
    {
        animSpeed = anim.speed;
        movementSpeed = GetComponent<AdvancedWalkerController>().getMovementSpeed();
        GetComponent<AdvancedWalkerController>().setMovementSpeed(10f);
        anim.speed = 2.5f;
        StartCoroutine(ResetSpeed());
    }

    [Command]
    public void CmdResetSpeed()
    {
        if (isServer)
        {
            RpcResetSpeed();
        }
    }

    [TargetRpc]
    public void RpcResetSpeed()
    {
        GetComponent<AdvancedWalkerController>().setMovementSpeed(movementSpeed);
        anim.speed = animSpeed;
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(3f);
        CmdResetSpeed();
    }

    IEnumerator skillTime(float time, GameObject target, int choice, float animSpeed)
    {
        yield return new WaitForSeconds(time);

        if (choice == 1)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
        }
        else if (choice == 2)
        {
            //target.GetComponent<AdvancedWalkerController>().setGravity(47f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
        }
        else if (choice == 3)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }
        else if (choice == 4)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }
    }

    IEnumerator disableParticle(int i, float time, Skills skillFX)
    {
        yield return new WaitForSeconds(time);

        if (skillFX.particles[i].GetComponent<DemoReactivator>() != null)
            skillFX.particles[i].GetComponent<DemoReactivator>().CancelInvoke();

        skillFX.particles[i].gameObject.SetActive(false);
    }

    IEnumerator rotate(Camera cam)
    {
        float rotation = 180f;
        while (true)
        {
            rotation += 5;
            cam.transform.rotation = Quaternion.Euler(new Vector3(rotation, rotation, rotation)); // rotate on y axis
            yield return new WaitForSeconds(0.001f);
        }

    }

    IEnumerator ZilchRecoverNumerator()
    {
        Debug.Log("Recovery");
        yield return new WaitForSeconds(3f);
        cam.GetComponent<Camera>().transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StopAllCoroutines();
    }


    [Command]
    public void CmdResetCdSkill()
    {
        if (isServer)
        {
            RpcResetSkill();
        }
    }

    [TargetRpc]
    public void RpcResetSkill()
    {
        GetComponent<SkillControls>().resetCD();
    }

    [Command]
    public void CmdUltiPoint()
    {
        if (isServer)
        {
            RpcUltiPoint();
        }
    }

    [TargetRpc]
    public void RpcUltiPoint()
    {
        GetComponent<SkillControls>().setUltiPoint(true);
    }

    [Command]
    public void CmdTakeTrixSkill()
    {
        RpcTakeTrixSkill();
    }

    [TargetRpc]
    public void RpcTakeTrixSkill()
    {
        GameObject target = gameObject;
        AdvancedWalkerController simp = GetComponent<AdvancedWalkerController>();
        simp = target.GetComponent<AdvancedWalkerController>();
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        SoundManager sound = target.GetComponent<SoundManager>();
        sound.adSrc.pitch = .2f;
        float speed = simp.getMovementSpeed();
        simp.setMovementSpeed(0f);
        GetComponent<LanSkillAnimation>().SpawnEffect(1, true);

        StartCoroutine(skillTime(3f, gameObject, 1, 0));
    }

    [Command]
    public void CmdTakeZilchSkill()
    {
        RpcTakeZilchSkill();
    }

    [TargetRpc]
    public void RpcTakeZilchSkill()
    {
        GetComponent<LanSkillAnimation>().SpawnEffect(1, true);
        StartCoroutine(rotate(cam.GetComponent<Camera>()));
        StartCoroutine(ZilchRecoverNumerator());
    }



    [Command]
    public void CmdTakeMazeSkill()
    {
        RpcTakeMazeSkill();
    }

    [TargetRpc]
    public void RpcTakeMazeSkill()
    {
        Animator anim = GetComponent<Animator>();
        AdvancedWalkerController simp = GetComponent<AdvancedWalkerController>();
        SoundManager sound = GetComponent<SoundManager>();
        Skills skillFX = GetComponentInChildren<Skills>();

        //float pitch = sound.adSrc.pitch;
        float speed = simp.getMovementSpeed();
        float jumpSpeed = simp.getJumpSpeed();
        sound.adSrc.pitch = .2f;
        simp.setMovementSpeed(1f);
        simp.setJumpSpeed(1f);

        //PLAY OBSTACLES
        GetComponent<LanSkillAnimation>().SpawnEffect(4, true);

        float normSpeed = anim.speed;
        anim.speed = .2f;

        StartCoroutine(skillTime(3f, gameObject, 3, normSpeed));
    }

    public void LanCastUltimate()
    {
        if (!canUlti)
        {
            return;
        }
        AdvancedWalkerController simp;
        Rigidbody rb;
        //MAZE-3 ZILCH-4 TRIX-0
        string myTag = gameObject.tag;
        switch (myTag)
        {
            case "Maze":
                simp = GetComponent<AdvancedWalkerController>();
                if (simp.IsGrounded())
                {
                    simp.setJumpSpeed(15f);
                    simp.jump();
                    GetComponent<LanSkillAnimation>().SpawnEffect(3, true);
                }
                simp.setJumpSpeed(15);
                simp.jumpNow();
                break;
            case "Zilch":
                simp = GetComponent<AdvancedWalkerController>();
                float normSpeed = anim.speed;
                float speed = simp.getMovementSpeed();
                simp.setMovementSpeed(25f);
                anim.speed = 3.5f;
                GetComponent<LanSkillAnimation>().SpawnEffect(4, true);
                StartCoroutine(StopZilchUlti(normSpeed, speed));
                break;
            case "Trix":
                simp = GetComponent<AdvancedWalkerController>();
                rb = GetComponent<Rigidbody>();
                Vector3 moveDirection = transform.InverseTransformDirection(rb.velocity);
                float dashForce = 600f;
                GetComponent<LanSkillAnimation>().SpawnEffect(0, true);
                rb.AddForce(moveDirection * dashForce, ForceMode.Impulse);
                break;
        }
        CmdUltiStatus(false);
    }

    IEnumerator StopZilchUlti(float animSpeed, float charSpeed)
    {
        yield return new WaitForSeconds(3f);
        anim.speed = animSpeed;
        GetComponent<AdvancedWalkerController>().setMovementSpeed(charSpeed);
    }

    #region Collider
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("sbTrix"))
        {
            Debug.Log("Hit sbTrix");
            CmdTakeTrixSkill();

        }
        else if (other.gameObject.tag.Equals("sbZilch"))
        {
            Debug.Log("Hit Zilch");
            CmdTakeZilchSkill();
            
        }
        else if (other.gameObject.tag.Equals("sbMaze"))
        {
            Debug.Log("Hit Maze");
            CmdTakeMazeSkill();
           
        }
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("s5Obs") && Stage5Handler.GetComponent<LanS5PickupHandler>().canPick)
        {
            Stage5Handler.GetComponent<LanS5PickupHandler>().CmdCanPick(false);
            Stage5Handler.GetComponent<LanS5PickupHandler>().CmdButtonValue(other.GetComponent<LanS5Object>().value);
            
            //GameObject.Find("GameHandler").GetComponent<ObjectSpawner>().currentSpawn--;
            Destroy(other.gameObject);
        }
    }
}
