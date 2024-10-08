using UnityEngine;

public class EntityMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Collider2D coll;

    public float speed = 2f;
    public Vector2 direction = Vector2.left;

    private Vector2 velocity;

    private bool spawning;
    private Vector2 spawnOrigin;
    public float spawnSpeed = 1f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        enabled = false;
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
        if (spawning) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            if ((rb.position - spawnOrigin).y >= 0.5f + GetComponent<CircleCollider2D>().radius) {
                spawning = false;
                coll.enabled = true;
            }
        } else {
            velocity.x = direction.x * speed;
            velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.GetContact(0).normal.x != 0) {
            direction = -direction;
        }
    }

    public void SpawnMovement() {
        spawning = true;
        enabled = true;
        spawnOrigin = rb.position;
        velocity.y = spawnSpeed;
        coll.enabled = false;
    }
}