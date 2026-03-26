using System;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;

    private void Awake()
    {
        if (_lifeController == null) _lifeController = GetComponentInParent<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out var pickable))
        {
            pickable.PickUp(this);
        }
    }

    public void AddCoins(int value)
    {
        AudioManager.Instance.PlaySFX("PickupCoin");
        CoinsManager.Instance.AddCoins(value);
    }

    public void AddHealth(int value)
    {
        AudioManager.Instance.PlaySFX("PickupHeart");
        _lifeController.AddHp(value);
    }

    public void AddTime(float value)
    {
        AudioManager.Instance.PlaySFX("PickupCoinTimer");
        TimeManager.Instance.AddTime(value);
    }
}