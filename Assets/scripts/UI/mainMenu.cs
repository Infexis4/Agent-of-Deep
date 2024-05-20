using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }



    public void Quit()
    {
        Application.Quit(); //build için çalýþan quit

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unity içinde quit yapar
#endif

    }

    public void infoMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
