using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer smallMario;
    public SpriteRenderer bigMario;
    public SpriteRenderer flowerMario;

    public bool big => bigMario.enabled;
    public bool small => smallMario.enabled;
    public bool flower => flowerMario.enabled;

    public void Hit() {
        if (big || flower) {
            Shrink();
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

        GameManager.Instance.ResetLevel();
    } 
}
