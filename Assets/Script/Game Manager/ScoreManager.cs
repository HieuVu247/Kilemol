using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI newHighScoreText;

    private int currentScore;
    private int highScore;

    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance của ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Khởi tạo điểm hiện tại và High Score
        currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Cập nhật UI
        UpdateScoreUI();
        newHighScoreText.gameObject.SetActive(false); // Ẩn thông báo New High Score ban đầu
    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();

        // Kiểm tra nếu đạt New High Score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            newHighScoreText.gameObject.SetActive(true); // Hiển thị thông báo New High Score
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
