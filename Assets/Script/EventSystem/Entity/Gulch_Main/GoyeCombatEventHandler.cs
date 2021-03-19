using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoyeCombatEventHandler : EntityEventHandler
{
    public override void OnDead()
    {
        GoyeCombat1 goye = transform.parent.GetComponent<GoyeCombat1>();
        Destroy(goye.transform.Find("Combat Field").gameObject);
        Camera.main.GetComponent<BasicFollower>().RestoreCameraFollowing();
        goye.GetComponent<EnemySaveData>().Save(false);
    }
}
