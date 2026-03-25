using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        LoadAudioSettings();
        if (AudioManager.Instance != null)
        {
            if (AudioManager.Instance.musicSource.clip == null)
            {
                AudioManager.Instance.PlayMusic("ThemeMenu");
            }

            if (AudioManager.Instance.musicSource.clip.name == "fearloop" ||
                AudioManager.Instance.musicSource.clip.name == "winnerloop")
            {
                AudioManager.Instance.StopAllAudioSource();
                AudioManager.Instance.PlayMusic("ThemeMenu");
            }
        }
    }

    private void LoadAudioSettings()
    {
        float masterVolume = 1f;
        float musicVolume = 1f;
        float sfxVolume = 1f;

        //Debug.Log("MAIN MENU Call LOADER AUDIO SETTINGS !!!");
        bool result = IOManager.Instance.LoadAudioSettings(ref masterVolume, ref musicVolume, ref sfxVolume);

        if (result)
        {
            AudioManager.Instance.SetMasterVolume(masterVolume);
            AudioManager.Instance.SetMusicVolume(musicVolume);
            AudioManager.Instance.SetSFXVolume(sfxVolume);
        }
        else
        {
            Debug.Log("ERROR AUDIO SETTINGS NOT LOADED !!!");
        }
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX("MouseClickSound");
    }

    public void StartGame()
    {
        //AudioManager.Instance.StopAllAudioSource();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

    }
}
