using UnityEngine;

public class PlayerWakeupState : PlayerState
{
    protected GameManager GM;
    private bool isSitup = false;
    private bool isStandup = false;
    public PlayerWakeupState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {
        this.GM = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    public override void Enter()
    {
        base.Enter();
        ResetControlVariables();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isSitup && !isStandup && normMovementInput.x != 0){
            player.Anim.Play(AlfAnimationHash.WAKEUP_STANDUP_0);
            isStandup = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    protected override void ResetControlVariables()
    {
        isSitup = false;
        isStandup = false;
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }


    /* Animation callback */
    public void CompleteWakeup(){
        if(!isSitup){
            isSitup = true;
        }
        else{
            stateMachine.SwitchState(player.idleState);
        }
    }

}
