using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private MagicSphere _magicSphere;
    [SerializeField] private float _fireInterval = 0.5f;
    [SerializeField] private GameObject _firePoint;

    [Header("Gizmos Settings")]
    [SerializeField] private float _maxDistance = 200f;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("Ref To UI Manager")]
    [SerializeField] private UIManager _uiManager;

    private PlayerController _pc;
    private Rotator _rotator;
    private Animator _anim;

    // Gizmos data
    private Camera _cam;
    private Ray _ray;
    private float _hitPointRadius = 0.15f;

    private Vector3 _direction;

    private float _lastShootTime;

    private void Awake()
    {
        if (_cam == null) _cam = Camera.main;
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_rotator == null) _rotator = GetComponent<Rotator>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        if (_pc == null) _pc = GetComponentInParent<PlayerController>();
        if (_uiManager == null) _uiManager = FindAnyObjectByType<UIManager>();
    }

    void OnDrawGizmos()
    {
        if (_cam == null) return;
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(_ray.origin, _ray.direction * _maxDistance);
        Gizmos.DrawRay(_firePoint.transform.position, _ray.direction * _maxDistance);

        if (Physics.Raycast(_ray, out RaycastHit hit, _maxDistance))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, _hitPointRadius);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CanShootNow()) return;
            if (_pc.isFiring) return;
            if (!_pc.isGrounded) return;
            if (_uiManager.isPaused) return;

            _pc.isFiring = true;

            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            Vector3 start = _firePoint.transform.position;

            _direction = _ray.direction;
            if (_rotator != null) _rotator.SetRotation(_direction);

            // trigger animazione (alla fine chiamerà ShootBullet tramite Animation Event)
            _anim.SetTrigger("MagicalAttack");
        }
    }

    public void ShootBullet()
    {
        _lastShootTime = Time.time;

        _pc.isFiring = false;

        _audioManager.PlaySFX("ShootFireBall");

        MagicSphere clonedMagicSphere = Instantiate(_magicSphere);
        clonedMagicSphere.transform.position = _firePoint.transform.position;
        clonedMagicSphere.Shoot(_direction);
    }

    public void TryToShoot()
    {
        if (CanShootNow())
        {
            ShootBullet();
        }
    }

    public bool CanShootNow()
    {
        return Time.time - _lastShootTime > _fireInterval;
    }
}

