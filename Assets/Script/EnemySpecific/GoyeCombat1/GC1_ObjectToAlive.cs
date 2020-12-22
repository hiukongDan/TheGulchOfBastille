public class GC1_ObjectToAlive : ObjectToAlive
{
    public GC1_BattleBeginState battleBeginState;
    public GC1_DefenceState defenceState;

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
}
