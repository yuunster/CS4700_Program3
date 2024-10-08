using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; private set; }
    public int coins { get; private set; }
    public int timeLeft { get; private set; }
    public int lives { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeLeftText;
    public TextMeshProUGUI livesText;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        TextMeshProUGUI[] texts = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach (TextMeshProUGUI text in texts) {
            switch (text.name) {
                case "Score":
                    scoreText = text;
                    break;
                case "Coins":
                    coinsText = text;
                    break;
                case "Time":
                    timeLeftText = text;
                    break;
                case "Lives":
                    livesText = text;
                    break;
            }
        }
        NewGame();
    }

    private void NewGame() {
        SceneManager.LoadScene("Level 1");
        score = 0;
        coins = 0;
        timeLeft = 400;
        lives = 3;
        Redraw();
        StartTime();
    }

    public void ResetLevel() {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives > 0) {
            SceneManager.LoadScene("Level 1");
            timeLeft = 400;
            Redraw();
        } else {
            GameOver();
        }
    }

    private void GameOver() {
        NewGame();
    }

    private void Redraw() {
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        timeLeftText.text = "Time: " + timeLeft;
        livesText.text = "Lives: " + lives;
    }

    public void IncreaseScore(int amount) {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void IncreaseCoins(int amount) {
        coins += amount;
        coinsText.text = "Coins: " + coins;
    }

    public void StartTime() {
        StartCoroutine(Timer());
    }

    public IEnumerator Timer() {
        while (timeLeft > 0) {
            yield return new WaitForSeconds(0.5f);
            timeLeft--;
            timeLeftText.text = "Time: " + timeLeft;
        }
    }
}