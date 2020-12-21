public class GC1_ObjectToAlive : ObjectToAlive
{
    public GC1_BattleBeginState battleBeginState;

    public void CompleteBattleBegin()
    {
        this.battleBeginState?.Complete();
    }
}
