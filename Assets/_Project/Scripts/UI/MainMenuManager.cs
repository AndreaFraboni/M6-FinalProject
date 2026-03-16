using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    AudioManager _audioManager;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        _audioManager.UpdateSlider();
    }

    private void Start()
    {
        if (_audioManager) _audioManager.PlayMusic("ThemeMenu");
    }

    public void PlayClickSound()
    {
        if (_audioManager) _audioManager.PlaySFX("MouseClickSound");
    }

    public void StartGame()
    {
        if (_audioManager) _audioManager.StopAllAudioSource();
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
