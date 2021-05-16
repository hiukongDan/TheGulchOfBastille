using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCombat1 : Entity
{
    #region REFERENCES
    public Player refPlayer { get; private set; }
    public DC1_ObjectToAlive dc1_ota { get; private set; }
    public Transform combatTrigger;
    //public GC1_ObjectToAlive gc1_ota { get; private set; }
    //private Transform combatField;
    #endregion

    #region STATE
    public DC1_IdleState idleState;
    public DC1_FlipState flipState;
    public DC1_TakeoffState takeoffState;
    public DC1_FlyIdleState flyIdleState;
    public DC1_DiveState diveState;
    public DC1_LandState landState;
    public DC1_SmashState smashState;
    #endregion

    #region STATE_DATA
    public IdleStateData idleStateData;
    public MeleeAttackStateData diveAttackData;
    public MeleeAttackStateData smashAttackData;
    #endregion

    public override void InitEntity()
    {
        base.InitEntity();
        combatTrigger.gameObject.SetActive(true);
        stateMachine?.ClearState();
        anim?.Play("idle_0");
        if(facingDirection < 0){
            Flip();
        }
        aliveGO.transform.position = initPosition.position;
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

        Gulch.GameEventListener.Instance.OnTakeDamage(new Gulch.TakeDamageData(aliveGO, Gulch.SpriteEffectType.Blink));
    }

    public void FaceToPlayer(){
        float tolerance = 0.3f;
        if(Mathf.Abs(aliveGO.transform.position.x - refPlayer.transform.position.x) > tolerance){
            FaceTo(refPlayer.transform.position);
        }
    }

    protected override bool FaceTo(Vector2 targetPos)
    {
        return base.FaceTo(targetPos);
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
        smashState = new DC1_SmashState(stateMachine, this, "smash_0", this, smashAttackData);

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
