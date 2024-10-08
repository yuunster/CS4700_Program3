using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    private GameManager gm;

    public float speed = 3f;
    public Vector2 direction = Vector2.left;
    public float bounceHeight = 10f;

    private Vector2 velocity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        velocity.y = -speed;
        gm = FindObjectOfType<GameManager>();
    }

    private void OnBecameVisible() {
        enabled = true;
    }

    private void OnBecameInvisible() {
        enabled = false;
    }

    private void OnEnable() {
        rb.WakeUp();
    }

    private void OnDisable() {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    private void FixedUpdate() {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * 4 * Time.fixedDeltaTime;
        velocity.y = Mathf.Max(velocity.y, -speed);

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            gm.IncreaseScore(100);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        } else if (collision.GetContact(0).normal.y < 0) {
            velocity.y = -bounceHeight;
        } else if (collision.GetContact(0).normal.y > 0) {
            velocity.y = bounceHeight;
        } else {
            Destroy(this.gameObject);
        }
    }
}