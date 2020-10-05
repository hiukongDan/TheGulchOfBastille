using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class E1_DeadState : DeadState
{
    protected Enemy1 enemy;
    public E1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DeadStateData stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(data.deadBloodParticle, enemy.aliveGO.transform.position, data.deadBloodParticle.transform.rotation);
        GameObject.Instantiate(data.deadChunkParticle, enemy.aliveGO.transform.position, data.deadChunkParticle.transform.rotation);
        GameObject.Destroy(enemy.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
