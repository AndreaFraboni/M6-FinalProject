using UnityEngine;

public class CannonTower : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("Cannon Tower Parameters")]
    [SerializeField] private Transform _cannonBaseAim;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _target;
    [SerializeField] protected float _fireRate = 2f;
    [SerializeField] private float _shootForce = 30f;
    [SerializeField] private bool _isActivated = false;

    private float _lastShoot = 0f;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();

        if (_target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(Tags.Player);
            if (go != null)
            {
                _target = go.transform;
            }
        }
    }

    private void Update()
    {
        if (_target)
        {
            if (_isActivated)
            {
                _cannonBaseAim.LookAt(_target.transform.position + Vector3.up);
                _cannonBaseAim.Rotate(0f, 180f, 0f, Space.Self); // inverto per correggere errore nella mesh del cannone 

                if (CanShoot())
                {
                    if (_target != null) Shoot();
                }
            }
        }
    }

    private bool CanShoot()
    {
        float shootTime = _lastShoot + _fireRate;
        if (Time.time >= shootTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Shoot()
    {
        _lastShoot = Time.time;

        _audioManager.PlaySFX("ShootFireBall");

        Vector3 dir = (_target.position - _firePoint.position).normalized;
        Quaternion rotationdesired = Quaternion.LookRotation(dir);
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, rotationdesired);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(dir * _shootForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (other.gameObject.CompareTag(Tags.Player))
        {
            _isActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) return;

        if (other.gameObject.CompareTag(Tags.Player))
        {
            _isActivated = false;
        }
    }
}