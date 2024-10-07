using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lives { get; private set; }

    private void Awake() {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        lives = 3;
        SceneManager.LoadScene("Level 1");
    }

    private void ResetLevel() {
        lives--;

        if (lives > 0) {
            SceneManager.LoadScene("Level 1");
        } else {
            GameOver();
        }
    }

    private void GameOver() {
        NewGame();
    }
}