using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void RestartScene()
    {
        GameManager.Instance.RestartScene();
    }
}
