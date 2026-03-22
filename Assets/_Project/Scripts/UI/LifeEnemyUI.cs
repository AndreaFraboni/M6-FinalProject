using UnityEngine;
using UnityEngine.UI;

public class LifeEnemyUI : MonoBehaviour
{
    [SerializeField] private Image _bar_lifeBarFillable;

    private LifeController _lifeController;

    private void Awake()
    {
        if (_lifeController == null) _lifeController = GetComponentInParent<LifeController>();
    }
    
    private void OnEnable()
    {
        if (_lifeController != null)
        {
            _lifeController.OnHealthChanged += OnChangeLife;
            OnChangeLife(_lifeController.GetHp(), _lifeController.GetMaxHp());
        }
    }

    private void OnDisable()
    {
        if (_lifeController != null) _lifeController.OnHealthChanged -= OnChangeLife;
    }

    public void OnChangeLife(int hp, int maxhp)
    {
        if (maxhp <= 0) return;
        _bar_lifeBarFillable.fillAmount = (float)hp / maxhp;
    }

}
