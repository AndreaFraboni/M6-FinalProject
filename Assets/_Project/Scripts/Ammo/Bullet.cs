using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("Explosion Damage")]
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private int _damage = 15;
    [SerializeField] private LayerMask _damageLayers;

    private bool _isExploded = false;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
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
