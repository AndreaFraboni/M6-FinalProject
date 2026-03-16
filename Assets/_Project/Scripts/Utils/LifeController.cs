using UnityEngine;
using UnityEngine.Events;
public class LifeController : MonoBehaviour
{
    [Header("Health Refs")]
    [SerializeField] private int _currenthp;
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private bool _fullHPOnStart = true;

    [Header("Unity Event on Health Event")]
    [SerializeField] private UnityEvent<int, int> _onHPChanged;
    [SerializeField] private UnityEvent _onDefeated;

    [Header("Player Refs")]
    [SerializeField] private PlayerController _pc;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        if (_pc == null) _pc = GetComponent<PlayerController>();
        if (_audioManager == null) _audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Getter
    public int GetHp() => _currenthp;
    public int GetMaxHp() => _maxHP;

    private void Start()
    {
        if (_fullHPOnStart) SetHp(_maxHP);
    }

    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHP);

        if (hp != _currenthp)
        {
            _currenthp = hp;

            _onHPChanged.Invoke(_currenthp, _maxHP);

            if (_currenthp <= 0)
            {
                _onDefeated.Invoke();
            }
        }
    }

    public void AddHp(int amount)
    {
        if (amount < 0)
        {
            _audioManager.PlaySFX("GetDamage");
        }
        else
        {
            _audioManager.PlaySFX("PickupHeart");
        }

        SetHp(_currenthp + amount);
    }

    public void TakeDamage(int damage)
    {
        AddHp(-damage);
    }

    public void Defeated()
    {
        Destroy(gameObject);
    }
}
