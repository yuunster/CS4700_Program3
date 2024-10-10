using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public static void Create(int amount, Vector3 worldPos) {
        GameObject instance = Instantiate(GameAssets.i.scorePopup, worldPos, Quaternion.identity);

        instance.GetComponent<TextMeshPro>().text = amount.ToString();

        Destroy(instance, 1f);
    }
}
