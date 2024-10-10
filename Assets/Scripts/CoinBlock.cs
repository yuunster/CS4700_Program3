using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    private GameManager gm;

    private bool active = true;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
    }
    public void Spawn() {
        if (active) {
            active = false;
            gm.IncreaseCoins(1);
            gm.IncreaseScore(200);
            GameObject instance = Instantiate(GameAssets.i.coin, transform.position, Quaternion.identity);
        }
    }
}
