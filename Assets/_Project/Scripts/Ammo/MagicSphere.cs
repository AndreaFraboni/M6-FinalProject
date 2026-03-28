using UnityEngine;
using static UnityEngine.UI.Image;

public class MagicSphere : MonoBehaviour
{    
    [Header("MagicSphere Parameters")]
    [SerializeField] private float _lifeSpan = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 10;

    public DamageTarget _damageTarget = DamageTarget.Enemy;

    [SerializeField] private GameObject _explosionWavePrefab;
    [SerializeField] private Material _explosionMaterial;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    private Rigidbody _rb;

    private bool _isExploded = false;

    private Vector3 _movedir;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnEnable()
    {
        Destroy(gameObject, _lifeSpan);
    }

    private void FixedUpdate()
    {
        if (_movedir != Vector3.zero)
        {
            _rb.MovePosition(transform.position + _movedir * (_speed * Time.fixedDeltaTime));
        }
    }

    public void Shoot(Vector3 dir)
    {
        if (dir.sqrMagnitude > 1f) dir.Normalize();
        _movedir = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AudioManager.Instance!=null) AudioManager.Instance.PlaySFX("MagicSpellExplode");

        Vector3 hitPoint = transform.position;

        SpawnExplosionWave(hitPoint);
        Explode();
    }

    private void SpawnExplosionWave(Vector3 position)
    {
        if (_explosionWavePrefab == null) return;
        GameObject Wave = Instantiate(_explosionWavePrefab, position, Quaternion.identity);
        Wave.GetComponent<ExplosionWave>().SetMaterialWave(_explosionMaterial);
        Wave.GetComponent<ExplosionWave>().SetDamageWave(_damage);
        Wave.GetComponent<ExplosionWave>().SetDamageTarget(_damageTarget);
    }

    private void Explode()
    {
        if (_isExploded) return;
        _isExploded = true;
        Destroy(gameObject);
    }
}
