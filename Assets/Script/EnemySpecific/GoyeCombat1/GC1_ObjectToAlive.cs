public class GC1_ObjectToAlive : ObjectToAlive
{
    public GC1_BattleBeginState battleBeginState;
    public GC1_DefenceState defenceState;
    public GC1_ParryState parryState;

    public void CompleteBattleBegin()
    {
        battleBeginState?.Complete();
    }

    public void CompleteDefence()
    {
        defenceState?.Complete();
    }

    public void ActiveDefenceCounterAttack()
    {
        defenceState?.ActiveCounterAttack();
    }
    public void DoParry() 
    {
        parryState?.DoParry();
    }

    public void CompleteParry()
    {
        parryState?.Complete();
    }
}
