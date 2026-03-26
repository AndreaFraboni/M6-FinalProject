using UnityEngine;

public class CoinTime : MonoBehaviour, IPickable
{
    [Header("COIN TIME parameters")]
    [SerializeField] private float _rotSpeed = 100f;
    [SerializeField] private float _addtime = 10;

    void Update()
    {
        transform.Rotate(_rotSpeed * Time.deltaTime, 0, 0);
    }

    public void PickUp(Picker collector)
    {
        collector.AddTime(_addtime);
        Destroy(gameObject);
    }
}
