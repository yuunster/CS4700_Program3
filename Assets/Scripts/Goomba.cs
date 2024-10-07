using UnityEngine;

public class Goomba : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            }
        }
    }

    private void Flatten() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        transform.localScale = new Vector3(1f, 0.5f, 1f);
        Destroy(gameObject, 0.5f);
    }
}
