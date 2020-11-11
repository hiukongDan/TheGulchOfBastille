using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_RecoverState : State
{
    protected SlowMutantElite1 enemy;
    public SME1_RecoverState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        var ota = (SME1_ObjectToAlive)enemy.objectToAlive;
        ota.recoverState = this;

        enemy.SetIsDamageable(false);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetIsDamageable(true);

        var ota = (SME1_ObjectToAlive)enemy.objectToAlive;
        ota.recoverState = null;
    }

    public void CompleteRecover()
    {
        stateMachine.SwitchState(enemy.walkState);
    }
}
