using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    protected WalkStateData data;

    protected Vector2 vectorWorkspace = new Vector2();

    protected float walkDurationTime;

    public WalkState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, WalkStateData walkData): base(stateMachine, entity, animBoolName)
    {
        data = walkData;
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();
        walkDurationTime = Random.Range(data.walkTimeMin, data.walkTimeMax);
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
        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
        DetectPlayerInMinAgro();
    }
}
