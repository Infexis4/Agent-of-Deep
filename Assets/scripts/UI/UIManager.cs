using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause Game")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over


    //Oyun sonu ekranýný aktifleþtirir
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }


    //oyun sonu fonksiyonlarý
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void Quit()
    {
        Application.Quit(); //build için çalýþan quit

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unity içinde quit yapar
        #endif

    }

    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //durum true ise oyunu durdurur false ise devam ettirir
        pauseScreen.SetActive(status);


        if(status)
                 Time.timeScale = 0;
        else
                 Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.02f);
    }
    #endregion
}
