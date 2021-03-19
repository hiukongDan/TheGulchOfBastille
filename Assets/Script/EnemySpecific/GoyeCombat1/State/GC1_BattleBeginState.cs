using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_BattleBeginState : State
{
    protected GoyeCombat1 enemy;
    protected float battleBeginDelay = 1f;

    protected bool isBattleBegin{get; private set;}

    public GC1_BattleBeginState(FiniteStateMachine stateMachine, Entity entity, string animName, GoyeCombat1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        isBattleBegin = false;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public IEnumerator BattleBegin()
    {
        // set playerCanMove false: use converseState
        enemy.refPlayer.stateMachine.SwitchState(enemy.refPlayer.cinemaState);

        yield return new WaitForSeconds(battleBeginDelay);

        var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemy.anim.SetBool("isBattleBegin", true);
        yield return new WaitUntil(() => isBattleBegin);


        // set player position
        // play ui fade in
        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));
        Vector3 goyePos = enemy.transform.Find("Combat Field/Goye Position").position;
        Vector3 playerPos = enemy.transform.Find("Combat Field/Player Position").position;
        enemy.refPlayer.SetPosition(playerPos);
        // set goye position
        enemy.objectToAlive.transform.position = goyePos;
        // play ui fade out
        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));

        // set goye rigidbodytype2d to dynamic
        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        stateMachine.SwitchState(enemy.runState);
        enemy.refPlayer.stateMachine.SwitchState(enemy.refPlayer.idleState);
    }

    public override void Complete()
    {
        isBattleBegin = true;
    }


}
