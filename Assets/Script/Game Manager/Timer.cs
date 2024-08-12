using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText; // Kéo thả UI Text vào đây trong Unity Editor
    public TMP_Text completionText; // Kéo thả UI Text để hiển thị thông báo hoàn thành vào đây
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        // Bắt đầu đếm thời gian khi game bắt đầu
        StartTimer();
        completionText.text = ""; // Đảm bảo thông báo hoàn thành trống khi bắt đầu
    }

    void Update()
    {
        // Cập nhật thời gian đã trôi qua nếu bộ đếm đang chạy
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();

            // Kiểm tra nếu thời gian đã đạt 30 giây
            if (elapsedTime >= 300f)
            {
                CompleteGame();
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f; // Reset thời gian về 0 khi bắt đầu
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
        completionText.text = ""; // Xóa thông báo hoàn thành khi reset
    }

    void UpdateTimerDisplay()
    {
        // Hiển thị thời gian dưới dạng phút:giây
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void CompleteGame()
    {
        StopTimer(); // Dừng bộ đếm thời gian
        completionText.text = "Hoàn thành trò chơi!"; // Hiển thị thông báo
        Time.timeScale = 0f;
    }

}
