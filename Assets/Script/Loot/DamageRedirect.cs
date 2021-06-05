using UnityEngine;

public class DamageRedirect : MonoBehaviour
{
    void Damage(CombatData data){
        transform.parent.SendMessage("Damage", data);
    }
}
