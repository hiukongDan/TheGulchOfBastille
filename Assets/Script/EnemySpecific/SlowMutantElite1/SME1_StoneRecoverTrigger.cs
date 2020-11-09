using UnityEngine;

public class SME1_StoneRecoverTrigger : MonoBehaviour
{
    private SlowMutantElite1 slowMutantElite;
    private bool isAwake;
    void Start()
    {
        slowMutantElite = transform.parent.parent.GetComponent<SlowMutantElite1>();
        isAwake = slowMutantElite.isAwake;

        if (isAwake)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (!isAwake)
            {
                isAwake = true;
                transform.parent.GetComponent<SlowMutantEliteEventHandler>().OnStoneRecover();
                Destroy(gameObject);
            }
        }
    }
}
