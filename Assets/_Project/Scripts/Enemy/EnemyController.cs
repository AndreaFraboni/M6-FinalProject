using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("Enemy Animation")]
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private Transform _target;
    //[SerializeField] private float _rotationSpeed = 5f;
    //[SerializeField] private float _speed = 2.0f;

    private Rigidbody _rb;

    private LifeController _lifeController;

    private CapsuleCollider _capsuleCollider;

    public bool isAlive = true;

    private Vector3 direction;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponentInParent<Rigidbody>();
        if (_lifeController == null) _lifeController = GetComponentInParent<LifeController>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        if (_enemyAnimation == null) _enemyAnimation = GetComponentInParent<EnemyAnimation>();
        if (_capsuleCollider == null) _capsuleCollider = GetComponent<CapsuleCollider>();

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
        if (_target == null || _rb == null)
        {
            direction = Vector3.zero;
            return;
        }

        Vector3 targetPosition = _target.position;
        direction = (targetPosition - _rb.position).normalized;
    }

    private void FixedUpdate()
    {
     //   if (isAlive) EnemyMovement();
    }

    void EnemyMovement()
    {
        //Quaternion _rotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.fixedDeltaTime * _rotationSpeed);
        //Vector3 velocity = direction * _speed;
        //_rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
    }

    public void DestroyGOEnemy()
    {
        Destroy(gameObject);
    }




    public void OnDefeated()
    {
        isAlive = false;

        _audioManager.PlaySFX("DeathSound");

        if (_capsuleCollider != null) _capsuleCollider.enabled = false;

        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        _enemyAnimation.SetBoolParam("isDying", true);
    }

}
