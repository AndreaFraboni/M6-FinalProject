using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Image _bar_lifeBarFillable;
    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private TextMeshProUGUI _currentCoinstext;

    [Header("Player movement parameters")]
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _smooth = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    [Header("UI Manager")]
    [SerializeField] private UIManager _UIManager;

    [Header("Player Animation")]
    [SerializeField] private PlayerAnimation _playerAnimation;

    [Header("OnCoinPickup Event")]
    [SerializeField] private UnityEvent<int> _onCoinPickup;

    private Mover _mover;
    private Rotator _rotator;
    private Rigidbody _rb;
    private CapsuleCollider _capsuleCollider;

    private float horizontal, vertical = 0f;
    private Vector3 currentDirection = Vector3.zero;

    private Camera _cam;
    private Ray _ray;

    public bool isAlive = true;
    public bool isGrounded = false;
    public bool isJump = false;
    public bool isRunning = false;
    public bool isFiring = false;
    public bool isFalling = false;

    private bool _deathPending = false;
    private bool _deathStarted = false;

    public int _currentCoins = 0;

    // Getter
    public Vector3 GetDirection() => currentDirection;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (_mover == null) _mover = GetComponent<Mover>();
        if (_rotator == null) _rotator = GetComponent<Rotator>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
        if (_UIManager == null) _UIManager = FindAnyObjectByType<UIManager>();
        if (_playerAnimation == null) _playerAnimation = GetComponentInParent<PlayerAnimation>();
        if (_capsuleCollider == null) _capsuleCollider = GetComponent<CapsuleCollider>();
        _cam = Camera.main;
    }

    void Update()
    {
        CheckInput();
        CheckRun();
        CheckJump();
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();

        if (isJump) Jump();

        if (_deathPending && isGrounded)
        {
            StartDeathAnimation();
        }
    }

    private void CheckInput()
    {
        if (!isAlive) return;
        if (isFiring) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cam.transform.forward * vertical + _cam.transform.right * horizontal;
        targetDirection.y = 0f;

        if (targetDirection.magnitude > 0.01f) targetDirection.Normalize();

        currentDirection = Vector3.Lerp(currentDirection, targetDirection, _smooth * Time.deltaTime);
    }

    private void CheckRun()
    {
        if (!isAlive) return;
        if (isFiring) return;

        isRunning = (Input.GetKey("left shift"));
    }

    private void CheckJump()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJump = true;
            _playerAnimation.SetBoolParam("isWalking", false);
            _playerAnimation.SetBoolParam("isRunning", false);
            _playerAnimation.TriggerJumpUp();
            isGrounded = false;
        }
    }

    private void Move()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (_mover != null && !isFiring)
        {
            if (isRunning)
            {
                _mover.SetSpeed(_speed * 2);
                _mover.SetAndNormalizeInput(currentDirection);
            }
            else
            {
                _mover.SetSpeed(_speed);
                _mover.SetAndNormalizeInput(currentDirection);
            }
        }
    }

    private void Rotation()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (currentDirection.sqrMagnitude < 0.0004f) return;

        if (_rotator != null) _rotator.SetRotation(currentDirection);
    }

    private void Jump()
    {
        if (!isAlive) return;
        if (isFiring) return;

        isJump = false;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void GetCoins(int coinValue)
    {
        _audioManager.PlaySFX("PickupCoin");
        _currentCoins++;
        _onCoinPickup.Invoke(_currentCoins);
    }

    public void PlayerHitByObject()
    {
        _audioManager.PlaySFX("GetDamage");
    }

    public void DestroyGOPlayer()
    {
        _UIManager.GameOver();
        Destroy(gameObject);
    }

    public void FootStepSound()
    {
        _audioManager.PlaySFX("FootStep");
    }
    public void OnChangeLife(int hp, int maxhp)
    {
        _lifeText.text = hp + "/" + maxhp;
        _bar_lifeBarFillable.fillAmount = (float)hp / maxhp;
    }

    private void StartDeathAnimation()
    {
        if (_deathStarted) return;

        _deathStarted = true;
        _deathPending = false;

        _playerAnimation.SetBoolParam("isDying", true);

        if (_capsuleCollider != null) _capsuleCollider.enabled = false;

        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }
    }
    public void OnDefeated()
    {
        isAlive = false;
        isFiring = false;

        _audioManager.PlaySFX("DeathSound");

        if (isGrounded)
        {
            StartDeathAnimation();
        }
        else
        {
            _deathPending = true;
        }
    }
}
