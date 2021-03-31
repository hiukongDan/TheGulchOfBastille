using UnityEngine;

public class SME1_StoneRecoverTrigger : MonoBehaviour
{
    private SlowMutantElite1 slowMutantElite;
    private bool isAwake;
    void Start()
    {
        

        // if (isAwake)
        // {
        //     Destroy(gameObject);
        // }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (!isAwake)
            {
                isAwake = true;
                transform.parent.GetComponent<SlowMutantEliteEventHandler>().OnStoneRecover();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void OnEnable() {
        Gulch.GameEventListener.Instance.OnPlayerDeadHandler += OnPlayerDead;

        slowMutantElite = transform.parent.parent.GetComponent<SlowMutantElite1>();
        isAwake = slowMutantElite.isAwake;
    }

    void OnDisable() {
        Gulch.GameEventListener.Instance.OnPlayerDeadHandler -= OnPlayerDead;
    }

    protected void OnPlayerDead(){
        GetComponent<Collider2D>().enabled = true;
        
    }
}
