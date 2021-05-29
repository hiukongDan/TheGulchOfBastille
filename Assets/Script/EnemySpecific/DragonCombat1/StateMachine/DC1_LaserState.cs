using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_LaserState : State
{
    protected DragonCombat1 enemy;
    protected DC1_Laser laser_obj;
    protected MeleeAttackStateData attackData;
    public DC1_LaserState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy, DC1_Laser laser_obj, MeleeAttackStateData laserAttackData)
         : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        this.laser_obj = laser_obj;
        this.attackData = laserAttackData;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public void CompletePrepareLaser(){
        laser_obj.gameObject.SetActive(true);
        laser_obj.InitiateLaser();
    }

    public override void Complete()
    {
        base.Complete();
        stateMachine.SwitchState(enemy.landState);
        laser_obj.HideLaser();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.dc1_ota.laserState = this;

        CombatData combatData = this.attackData.GetCombatData();
        combatData.from = enemy.aliveGO;

        enemy.laser_obj?.SetCombatData(combatData);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.dc1_ota.laserState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DoChecks();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {

    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
