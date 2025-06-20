using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SaveManager.ShouldLoadGame = true;
        SceneManager.LoadScene("Game");
    }
}
