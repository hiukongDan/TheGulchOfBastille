using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMutantEliteEventHandler : EntityEventHandler
{
    public override void OnDead()
    {
        base.OnDead();
    }

    public void OnStoneRecover()
    {
        var slowMutantElite = transform.parent.GetComponent<SlowMutantElite1>();
        slowMutantElite.stoneState.TriggerRecover();
    }
}
