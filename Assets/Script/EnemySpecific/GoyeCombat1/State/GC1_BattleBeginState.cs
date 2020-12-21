using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_BattleBeginState : State
{
    protected GoyeCombat1 enemy;
    protected float battleBeginDelay = 1f;

    protected bool isBattleBegin;

    private Player player;

    public GC1_BattleBeginState(FiniteStateMachine stateMachine, Entity entity, string animName, GoyeCombat1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        isBattleBegin = false;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        enemy.gc1_ota.battleBeginState = this;
        enemy.anim.SetBool("isBattleBegin", false);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.gc1_ota.battleBeginState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isBattleBegin)
        {
            stateMachine.SwitchState(enemy.runState);
            // set playerCanMove true: use EndConverse
            player.OnEndConversation();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public IEnumerator BattleBegin()
    {
        // set playerCanMove false: use converseState
        player.stateMachine.SwitchState(player.converseState);

        yield return new WaitForSeconds(battleBeginDelay);
        enemy.anim.SetBool("isBattleBegin", true);
    }

    public override void Complete()
    {
        isBattleBegin = true;
    }
}
