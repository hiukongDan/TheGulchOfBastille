using UnityEngine;

public class Uilos : MonoBehaviour
{
    private float oldGravity;
    private Rigidbody2D rb;
    private bool isChanged = false;
    public int perUilosWorth = 1;
    public float uilosFalldownGravityScale = 2.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldGravity = rb.gravityScale;
        rb.gravityScale = uilosFalldownGravityScale;
        rb.velocity = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(8f, 12f));
    }

    void Update()
    {
        if(!isChanged && rb.velocity.y <= 0f)
        {
            rb.gravityScale = oldGravity;
            isChanged = true;
        }
    }

    void LateUpdate() {
        if(isChanged){
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Fools_Gold_Pendant)){
                rb.gravityScale = 0;
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 force = ItemData.WearableItemBuffData.Drawf_Ring_forceTowardsPlayer;
                rb.AddForce(direction * new Vector2(Random.Range(force.x/2, force.x), 
                    Random.Range(force.y/2, force.y)), ForceMode2D.Force);
            }
        }
    }

    void OnDisable() {
        Destroy(gameObject);    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && isChanged)
        {
            //collision.gameObject.SendMessage("Uilos", 1);
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Drawf_Ring)){
                perUilosWorth = (int)ItemData.WearableItemBuffData.Fools_Gold_Pendant_uilosWorth;
            }
            other.gameObject.GetComponent<Player>().OnAquireUilos(perUilosWorth);
            Destroy(gameObject);
        }
    }
}
