using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoyeCombatEventHandler : EntityEventHandler
{

    private GC1_CombatTrigger combatTrigger;
    private GoyeCombat1 goyeCombat;

    private void Awake(){
        combatTrigger = transform.parent.GetComponentInChildren<GC1_CombatTrigger>();
        goyeCombat = GetComponentInParent<GoyeCombat1>();
    }
    private void OnEnable() {
        if(goyeCombat.GetComponent<EnemySaveData>().IsAlive()){
            combatTrigger.enabled = true;
            combatTrigger.ResetTrigger();
        }
    }

    public override void OnDead()
    {
        GoyeCombat1 goye = transform.parent.GetComponent<GoyeCombat1>();
        Destroy(goye.transform.Find("Combat Field").gameObject);
        Camera.main.GetComponent<BasicFollower>().RestoreCameraFollowing();
        goye.GetComponent<EnemySaveData>().Save(false);
    }
}
