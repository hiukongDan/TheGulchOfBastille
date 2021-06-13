using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChase2 : Entity
{
    #region REFERENCES
    public Player refPlayer { get; private set; }
    public Transform combatTrigger;
    public DC2_ObjectToAlive dc2_ota { get; private set; }
    //private Transform combatField;
    #endregion

    #region TRANSFORM REFERENCES
    public Transform[] waypoints;
    public DC1_Laser laser_obj;
    #endregion

    #region STATE
    public DC2_SleepState sleepState;
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
        // ignored
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

        this.dc2_ota = (DC2_ObjectToAlive)objectToAlive;

        this.sleepState = new DC2_SleepState(stateMachine, this, "sleep_0", this);

        stateMachine.Initialize(sleepState);
    }

    public void Tmp_InitCombat(){
        StartCoroutine(onWakeup());
    }

    protected override void Update()
    {
        base.Update();
    }

    void OnAnimatorMove()
    {
        rb.velocity = anim.deltaPosition / Time.deltaTime;
    }

    IEnumerator onWakeup(){
        refPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        Camera.main.GetComponent<BasicFollower>().UpdateCameraFollowing(aliveGO.transform);
        anim.Play("wake_0");
        refPlayer.stateMachine.SwitchState(refPlayer.cinemaState);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));

        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));
        // stateMachine.SwitchState(chaseState);
        Camera.main.GetComponent<BasicFollower>().RestoreCameraFollowing();
        refPlayer.stateMachine.SwitchState(refPlayer.idleState);
    }
}
