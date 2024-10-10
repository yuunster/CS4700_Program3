using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public float speed = 1f; 
    public static void Create(int amount, Vector3 worldPos) {
        GameObject instance = Instantiate(GameAssets.i.scorePopup, worldPos, Quaternion.identity);

        instance.GetComponent<TextMeshPro>().text = amount.ToString();

        Destroy(instance, 1f);
    }

    public void FixedUpdate() {
        transform.position += new Vector3(0, speed) * Time.deltaTime;
    }
}
