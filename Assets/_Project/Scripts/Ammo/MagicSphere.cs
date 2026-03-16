using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    [Header("MagicSphere Parameters")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeSpan = 5f;
    [SerializeField] private float _speed = 10f;

    [Header("MagicSphere Damage Around impact point")]
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private LayerMask _damageLayers;

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
        _audioManager.PlaySFX("MagicSpellExplode");
        Explode();
    }

    private void Explode()
    {
        if (_isExploded) return;
        _isExploded = true;

        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _damageLayers);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<LifeController>(out LifeController life))
            {
                life.TakeDamage(_damage);
            }
        }
        Destroy(gameObject);
    }
}
