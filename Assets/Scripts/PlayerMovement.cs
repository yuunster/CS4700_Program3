using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera m_camera;
    private Rigidbody2D rb;
    Player player;
    public GameObject projectile;

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    public int maxAmmo = 2;
    public int reloadTime = 2;
    private int ammo = 2;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update() {
        HorizontalMovement();
        if (player.flower) {
            Shooting();
        }

        grounded = rb.Raycast(Vector2.down);

        if (grounded) {
            GroundedMovement();
        }

        ApplyGravity();
    }
    private void FixedUpdate() {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rb.MovePosition(position);
    }

    private void HorizontalMovement() {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        
        if (rb.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0;
        }

        if (inputAxis > 0f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (inputAxis < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void GroundedMovement() {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void Shooting() {
        if (Input.GetButtonDown("Fire1")) {
            if (ammo > 0) {
                ammo--;
                GameObject instance = Instantiate(projectile, rb.position + new Vector2(transform.localScale.x * 0.5f, 0), Quaternion.identity);
                instance.GetComponent<Projectile>().direction = transform.localScale;
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload() {
        yield return new WaitForSeconds(reloadTime);
        ammo++;
    }

    private void ApplyGravity() {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            if (transform.DotTest(collision.transform, Vector2.down)) {
                velocity.y = jumpForce / 2f;
            }
        } else if (collision.gameObject.layer != LayerMask.NameToLayer("Powerup") && velocity.y > 0f && rb.Raycast(Vector2.up)) {
            velocity.y = 0f;
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Powerup")) {
            switch (collision.gameObject.tag) {
                case "Mushroom":
                    player.Grow();
                    Destroy(collision.gameObject);
                    break;
                case "Flower":
                    player.Flower();
                    Destroy(collision.gameObject);
                    break;
            }
        }

    }
}
