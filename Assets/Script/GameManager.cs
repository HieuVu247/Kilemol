using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeTimeGradually());
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator ResumeTimeGradually()
    {
        float resumeTime = 0.1f;
        while (Time.timeScale < 1f)
        {
            Time.timeScale += resumeTime;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 1f;
    }
}
