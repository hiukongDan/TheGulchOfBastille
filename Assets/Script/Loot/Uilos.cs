using UnityEngine;

public class Uilos : MonoBehaviour
{
    private float oldGravity;
    private Rigidbody2D rb;
    private bool isChanged = false;
    public int perUilosWorth = 1;
    public float uilosFalldownGravityScale = 2.5f;
    public Vector2 forceTowardsPlayer = new Vector2(1,0.5f);
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
            // rb.gravityScale = oldGravity;
            isChanged = true;
        }
    }

    void LateUpdate() {
        if(isChanged){
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Fools_Gold_Pendant)){
                rb.gravityScale = 0;
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.AddForce(direction * new Vector2(Random.Range(forceTowardsPlayer.x/2, forceTowardsPlayer.x), 
                    Random.Range(forceTowardsPlayer.y/2, forceTowardsPlayer.y)), ForceMode2D.Force);
            }
            else{
                rb.gravityScale = uilosFalldownGravityScale;
            }
        }
    }

    void OnDisable() {
        Destroy(gameObject);    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.SendMessage("Uilos", 1);
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Drawf_Ring)){
                perUilosWorth = 2;
            }
            collision.gameObject.GetComponent<Player>().OnAquireUilos(perUilosWorth);
            Destroy(gameObject);
        }
    }
}
