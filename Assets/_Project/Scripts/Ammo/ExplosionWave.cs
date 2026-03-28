using System.Collections;
using UnityEngine;

public class ExplosionWave : MonoBehaviour
{
    [SerializeField] private float _maxRadius = 5f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Renderer _renderer;

    public DamageTarget _damageTarget = DamageTarget.Enemy;

    public int _damage = 10;

    public float speed = 1f;

    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        StartCoroutine(SphereScaling());
    }

    private IEnumerator SphereScaling()
    {
        float time = 0f;
        float diameter = _maxRadius * 2f;

        while (time < _duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp(time / _duration, 0f, 1f);
            float scale = diameter * t;
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        Destroy(gameObject);
    }

    public void SetDamageWave(int damage)
    {
        _damage = damage;
    }

    public void SetMaterialWave(Material material)
    {
        if (_renderer != null)
        {
            Material _material = new Material(material);
            _renderer.material = _material;
            Color _baseColor = _material.color;
        }
    }
    public void SetDamageTarget(DamageTarget damageTarget)
    {
        _damageTarget = damageTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LifeController>(out LifeController life))
        {
            switch (_damageTarget)
            {
                case DamageTarget.Enemy:
                    if (other.CompareTag("Enemy"))
                    {
                        life.TakeDamage(_damage);
                    }
                    break;

                case DamageTarget.Player:
                    if (other.CompareTag("Player"))
                    {
                        life.TakeDamage(_damage);
                    }
                    break;
            }
        }
    }

}
