using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void MainMenu() 
    {
        SceneManager.LoadScene("Select level");
    }
}
