using UnityEngine;

public class CoinTime : MonoBehaviour, IPickable
{
    [Header("COIN TIME parameters")]
    [SerializeField] private float _rotSpeed = 100f;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _addtime = 10;

    private void Awake()
    {
        if (_timer == null)
        {
            _timer = FindAnyObjectByType<Timer>();
        }
    }

    void Update()
    {
        transform.Rotate(_rotSpeed * Time.deltaTime, 0, 0);
    }


    public void PickUp(Collector collector)
    {
        collector.AddTime(_addtime);
        Destroy(gameObject);
    }
}
