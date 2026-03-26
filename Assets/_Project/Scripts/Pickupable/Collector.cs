using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<int> OnCoinPickUp;

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out var pickable))
        {
            pickable.PickUp(this);
        }
    }

    public void AddCoins(int amount)
    {
        // AudioManager.Instance.PlaySFX("PickupCoin");      
    }

    public void AddHealth(int amount)
    {

    }

    public void AddTime(float amount)
    {

    }
}