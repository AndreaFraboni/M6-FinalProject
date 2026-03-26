using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("TIMER SETTINGS")]
    [SerializeField] private float _countDown = 600f; // Durata del timer in secondi 600f = 10 minuti
    [SerializeField] private float _currentTime;

    public GameObject gameOver;
    public GameObject menuGameOver;

    public Action<float> OnTimeChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        OnTimeChanged?.Invoke(_currentTime);
        _currentTime = _countDown;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        TimeUpdate();

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            Invoke("GameOver", 0.5f);
        }
    }

    private void TimeUpdate()
    {
        int secondiTrascorsi = (int)_currentTime;
        OnTimeChanged?.Invoke(_currentTime);
    }

    public void AddTime(float value)
    {
        AudioManager.Instance.PlaySFX("PickupCoinTimer");
        _currentTime += value;
        if (_currentTime >= _countDown) _currentTime = _countDown;
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();
    }
}



