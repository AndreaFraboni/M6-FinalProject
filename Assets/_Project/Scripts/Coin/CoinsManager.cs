using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    [Header("Coin Manager referements")]
    [SerializeField] private TextMeshProUGUI _currentCoinstext;
    [SerializeField] private PlayerController _pc;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("UI Manager")]
    [SerializeField] private UIManager _UIManager;

    [Header("Coins State")]
    public int _currentCoins = 0;
    public int _coinsToPickup = 100;

    [Header("Game Completed Parameters")]
    public GameObject Door;
    public bool levelcompleted = false;

    private void Awake()
    {
        if (_pc == null) _pc = GetComponent<PlayerController>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        if (_UIManager == null) _UIManager = FindAnyObjectByType<UIManager>();
    }

    private void OnEnable()
    {
        if (_pc != null)
        {
            _currentCoins = _pc._currentCoins;
            _currentCoinstext.text = $"{_currentCoins}/{_coinsToPickup}";
        }
    }

    private void Update()
    {
        if (_currentCoins >= _coinsToPickup && !levelcompleted)
        {
            levelcompleted = true;
            _audioManager.PlaySFX("WinSound");
            Door.SetActive(false);
        }
    }

    public void OnCoinPickup(int currentcoins)
    {
        _currentCoins = currentcoins;
        _currentCoinstext.text = $"{currentcoins}/{_coinsToPickup}";
    }
}
