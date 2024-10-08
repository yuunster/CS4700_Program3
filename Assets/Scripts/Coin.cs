using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpHeight = 10f;
    public float gravity = 5f;
    private Vector2 velocity;
    private Vector2 origin;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        velocity.y = jumpHeight;
        origin = rb.position;
    }
    private void FixedUpdate() {
        velocity.y += Physics2D.gravity.y * gravity * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (rb.position.y < origin.y) {
            Destroy(this.gameObject);
        }
    }
}
