using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private int _damage = 15;

    public DamageTarget _damageTarget = DamageTarget.Player;

    [SerializeField] private GameObject _explosionWavePrefab;
    [SerializeField] private Material _explosionMaterial;

    private bool _isExploded = false;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("MagicSpellExplode");

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
