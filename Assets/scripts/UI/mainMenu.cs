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
        Application.Quit(); //build i�in �al��an quit

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unity i�inde quit yapar
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
