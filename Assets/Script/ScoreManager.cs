using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText; // TextMeshPro UI để hiển thị điểm
    private int score = 0;

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

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "" + score;
    }
}
