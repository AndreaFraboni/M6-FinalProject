using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("TIMER SETTINGS")]
    [SerializeField] private float _countDown = 600f; // Durata del timer in secondi 600f = 10 minuti
    [SerializeField] private float _currentTime;
    [SerializeField] private TextMeshProUGUI _currentTimetext;
    [SerializeField] private UIManager _UIManager;

    public GameObject gameOver;
    public GameObject menuGameOver;

    AudioManager _audioManager;

    private void Awake()
    {
        _currentTimetext.text = $"{(int)_currentTime} s";
        _currentTime = _countDown;
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        TimeManager();

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            Invoke("GameOver", 0.5f);
        }
    }

    private void TimeManager()
    {
        int secondiTrascorsi = (int)_currentTime;
        _currentTimetext.text = $"{secondiTrascorsi} s";
    }

    public void AddTime(float value)
    {
        _audioManager.PlaySFX("PickupCoinTimer");
        _currentTime += value;
        if (_currentTime >= _countDown) _currentTime = _countDown;
    }

    public void GameOver()
    {
        _UIManager.GameOver();
    }
}



