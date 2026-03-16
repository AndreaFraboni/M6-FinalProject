using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // UI referements
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject menuGameOver;
    public GameObject WinnerBanner;
    public GameObject menuWinner;

    public bool isPaused = false;
    AudioManager _audioManager;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        _audioManager.UpdateSlider();
    }
    private void Start()
    {
        _audioManager.PlayMusic("ThemeGame");
    }

    public void PlayClickSound()
    {
        _audioManager.PlaySFX("MouseClickSound");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Restart()
    {
        _audioManager.StopAllAudioSource();
        gameOver.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        _audioManager.StopAllAudioSource();

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        _audioManager.StopAllAudioSource();
        gameOver.SetActive(true);
        Invoke("ShowGameOverMenu", 1f);
    }

    public void ShowGameOverMenu()
    {
        _audioManager.PlayMusic("GameOverMusic");
        gameOver.SetActive(false);
        Time.timeScale = 0;
        menuGameOver.SetActive(true);
    }
    public void Winner()
    {
        _audioManager.StopAllAudioSource();
        WinnerBanner.SetActive(true);
        Invoke("ShowWinnerMenu", 1f);
    }

    public void ShowWinnerMenu()
    {
        WinnerBanner.SetActive(false);
        _audioManager.PlayMusic("WinnerMusic");
        Time.timeScale = 0;
        menuWinner.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		     Application.Quit();
#endif
        }
    }
}
