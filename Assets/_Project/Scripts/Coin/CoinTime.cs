using UnityEngine;

public class CoinTime : MonoBehaviour
{
    [Header("COIN TIME parameters")]
    [SerializeField] private float _coinRotSpeed = 100f;
    [SerializeField] private Timer _timer;
    [SerializeField] private float addtime = 10;

    private void Awake()
    {
        if (_timer == null)
        {
            _timer = FindAnyObjectByType<Timer>();
        }
    }

    void Update()
    {
        transform.Rotate(_coinRotSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            _timer.AddTime(addtime);
            Destroy(gameObject);
        }
    }

}
