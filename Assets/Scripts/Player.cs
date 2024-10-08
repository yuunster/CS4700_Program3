using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gm;

    public SpriteRenderer smallMario;
    public SpriteRenderer bigMario;
    public SpriteRenderer flowerMario;

    public bool big => bigMario.enabled;
    public bool small => smallMario.enabled;
    public bool flower => flowerMario.enabled;

    public bool invincible = false;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
    }
    public void Hit() {
        if (invincible) return;

        if (big || flower) {
            Shrink();
            StartCoroutine(Invincible());
        } else {
            Death();
        }
    }

    private void Shrink() {
        bigMario.enabled = false;
        smallMario.enabled = true;
        flowerMario.enabled = false;
    }

    public void Grow() {
        bigMario.enabled = true;
        smallMario.enabled = false;
    }

    public void Flower() {
        if (big) {
            bigMario.enabled = false;
            smallMario.enabled = false;
            flowerMario.enabled = true;
        } else {
            Grow();
        }
    }

    private void Death() {
        smallMario.enabled = false;
        bigMario.enabled = false;

        gm.ResetLevel();
    } 

    private IEnumerator Invincible() {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }
}
