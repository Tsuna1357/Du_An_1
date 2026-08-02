using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Chơi game
    public void PlayGame()
    {
        SceneManager.LoadScene("map1");
    }

    // Thoát game
    public void ExitGame()
    {
        Application.Quit();
    }
}