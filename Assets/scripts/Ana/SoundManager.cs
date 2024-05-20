using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //yeni seviyeye girdiðimizde objeyi korur
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //kopyalanmýþ sesleri siler
        else if (instance != null && instance != this)
            Destroy(gameObject);

        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }


    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }


    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.2f, "musicVolume", _change, musicSource);

    }

    private void ChangeSourceVolume(float baseVolume, string volumName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumName, 1);
        currentVolume += change;

        // min-max deðere ulaþýp ulaþmadýðýný kontrol eder
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //son deðeri kaydeder
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //son ses deðerlerini kaydeder
        PlayerPrefs.SetFloat(volumName, currentVolume);
    }

   
}
