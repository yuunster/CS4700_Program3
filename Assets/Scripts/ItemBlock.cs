using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public GameObject item;

    private bool active = true;

    public void Spawn() {
        if (active) {
            active = false;
            GameObject instance = Instantiate(item, transform.position, Quaternion.identity);
            instance.GetComponent<EntityMovement>().SpawnMovement();
        }
    }
}
