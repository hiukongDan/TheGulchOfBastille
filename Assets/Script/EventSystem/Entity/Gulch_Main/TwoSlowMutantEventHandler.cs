using Gulch;

public class TwoSlowMutantEventHandler : EntityEventHandler
{
    public override void OnDead()
    {
        GulchMainEventListener.Instance.OnSlay_SlowMutant_TrainingGround();
    }
}
