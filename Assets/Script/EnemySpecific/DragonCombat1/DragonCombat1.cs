using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCombat1 : Entity
{
    #region REFERENCES
    public Player refPlayer { get; private set; }
    public DC1_ObjectToAlive dc1_ota { get; private set; }
    public DragonChase2 dragonChase;
    public Transform combatTrigger;
    //public GC1_ObjectToAlive gc1_ota { get; private set; }
    //private Transform combatField;
    #endregion

    #region TRANSFORM REFERENCES
    public Transform[] laserLandingPositions;
    public DC1_Laser laser_obj;
    #endregion

    #region STATE
    public DC1_IdleState idleState;
    public DC1_FlipState flipState;
    public DC1_TakeoffState takeoffState;
    public DC1_FlyIdleState flyIdleState;
    public DC1_DiveState diveState;
    public DC1_LandState landState;
    public DC1_SmashState smashState;
    public DC1_LaserPositionState laserPositionState;
    public DC1_LaserState laserState;
    public DC1_DieState dieState;
    #endregion

    #region STATE_DATA
    public IdleStateData idleStateData;
    public MeleeAttackStateData diveAttackData;
    public MeleeAttackStateData smashAttackData;
    public MeleeAttackStateData laserAttackData;
    public GameObject smashDustPref;
    #endregion

    public override void InitEntity()
    {
        // Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().PrevSceneCode);
        if (!GetComponent<EnemySaveData>().IsAlive() || dragonChase.GetComponent<EnemySaveData>().IsAlive())
        {
            aliveGO.SetActive(false);
        }
        else{
            aliveGO.SetActive(true);
            Reset();
        }

        aliveGO.transform.position = initPosition.transform.position;
        //base.InitEntity();
        
        combatTrigger.gameObject.SetActive(true);
        stateMachine?.ClearState();
        anim?.Play("idle_0");
        if(facingDirection < 0){
            Flip();
        }
        aliveGO.transform.position = initPosition.position;

        laser_obj?.HideLaser();
    }

    protected override void Awake()
    {
        base.Awake();
        /* --------- ASIGN REFERENCEs HERE --------------*/
        refPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    protected override void Damage(CombatData combatData)
    {
        base.Damage(combatData);

        if(isDead && stateMachine.currentState == dieState){
            return;
        }
        else if(isDead){
            stateMachine.SwitchState(dieState);
        }

        Gulch.GameEventListener.Instance.OnTakeDamage(new Gulch.TakeDamageData(aliveGO, Gulch.SpriteEffectType.Blink));
    }

    public void FaceTo(Vector2 targetPos){
        faceTo(targetPos);
    }
    public void FaceToPlayer(){
        float tolerance = 0.3f;
        if(Mathf.Abs(aliveGO.transform.position.x - refPlayer.transform.position.x) > tolerance){
            faceTo(refPlayer.transform.position);
        }
    }

    protected override bool faceTo(Vector2 targetPos)
    {
        return base.faceTo(targetPos);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void KnockBack(CombatData combatData)
    {
        base.KnockBack(combatData);
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Start()
    {
        base.Start();

        this.dc1_ota = (DC1_ObjectToAlive)objectToAlive;

        // the final parameter and be refered in the class body
        idleState = new DC1_IdleState(stateMachine, this, "idle_0", idleStateData, this);
        flipState = new DC1_FlipState(stateMachine, this, "flip_0", this);
        takeoffState = new DC1_TakeoffState(stateMachine, this, "takeoff_0", this);
        flyIdleState = new DC1_FlyIdleState(stateMachine, this, "fly_idle_0", this);
        diveState = new DC1_DiveState(stateMachine, this, "dive_0", this, diveAttackData);
        landState = new DC1_LandState(stateMachine, this, "land_0", this);
        smashState = new DC1_SmashState(stateMachine, this, "smash_0", this, smashAttackData, smashDustPref);
        laserPositionState = new DC1_LaserPositionState(stateMachine, this, "fly_idle_0", this);
        laserState = new DC1_LaserState(stateMachine, this, "laser_0", this, laser_obj, laserAttackData);
        dieState = new DC1_DieState(stateMachine, this, "fly_idle_0", this);

        // stateMachine.Initialize(idleState);
    }

    public void Tmp_InitCombat(){
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    void OnAnimatorMove()
    {
        rb.velocity = anim.deltaPosition / Time.deltaTime;
    }
}
