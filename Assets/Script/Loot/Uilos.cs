using UnityEngine;

public class Uilos : MonoBehaviour
{
    private float oldGravity;
    private Rigidbody2D rb;
    private bool isChanged;
    void Start()
    {
        isChanged = false;
        rb = GetComponent<Rigidbody2D>();
        oldGravity = rb.gravityScale;
        rb.gravityScale = 2.5f;
        rb.velocity = new Vector2(Random.Range(-0.4f, 0.4f), 10f);
    }

    void Update()
    {
        if(!isChanged && rb.velocity.y <= 0f)
        {
            rb.gravityScale = oldGravity;
            isChanged = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.SendMessage("Uilos", 1);
            collision.gameObject.GetComponent<Player>().OnAquireUilos(1);
            Destroy(gameObject);
        }
    }
}
