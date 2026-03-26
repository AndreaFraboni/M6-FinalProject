using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currenCoinsText;

    private void Start()
    {
        if (CoinsManager.Instance != null)
        {
            CoinsManager.Instance.OnCoinsUpdated += UpdateCoinsText;
            UpdateCoinsText(CoinsManager.Instance._currentCoins, CoinsManager.Instance._coinsToPickup);
        }
        else
        {
            Debug.LogError("Timemanager: is NULL ?????");
        }

    }

    private void OnDisable()
    {
        if (CoinsManager.Instance != null)
        {
            CoinsManager.Instance.OnCoinsUpdated -= UpdateCoinsText;
        }
    }

    private void UpdateCoinsText(int coins,int coinsToPickup)
    {
        if (coins <= 0) return;
        if (_currenCoinsText != null) _currenCoinsText.text = $"{coins}/{coinsToPickup}";
    }

}
