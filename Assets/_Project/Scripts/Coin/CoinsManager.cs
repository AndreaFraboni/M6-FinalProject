using System;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance { get; private set; }

    [Header("Coins State")]
    public int _currentCoins = 0;
    public int _coinsToPickup = 100;

    [Header("Game Completed Parameters")]
    public GameObject Door;
    public bool levelcompleted = false;

    public Action<int, int> OnCoinsUpdated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        _currentCoins += amount;
        OnCoinsUpdated?.Invoke(_currentCoins, _coinsToPickup);
        if (_currentCoins >= _coinsToPickup && !levelcompleted)
        {
            levelcompleted = true;
            AudioManager.Instance.PlaySFX("WinSound");
            Door.SetActive(false);
        }
    }
}
