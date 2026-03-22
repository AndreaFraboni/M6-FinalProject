using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LifePlayerUI : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private TextMeshProUGUI _currenLifeText;
    [SerializeField] private Image _bar_lifeBarFillable;

    private void Awake()
    {
        if (_lifeController == null) _lifeController = GetComponentInParent<LifeController>();
    }

    private void OnEnable()
    {
        if (_lifeController != null)
        {
            _lifeController.OnHealthChanged += UpdateLifeText;
            UpdateLifeText(_lifeController.GetHp(), _lifeController.GetMaxHp()); // forzo lettura e aggiornamento UI del numero di vite attuale
        }
        else
        {
            Debug.LogError("LifeUI: LifeController non trovato!");
        }
    }

    private void OnDisable()
    {
        if (_lifeController != null) _lifeController.OnHealthChanged -= UpdateLifeText;
    }

    private void UpdateLifeText(int lifeNum, int maxhealth)
    {
        if (maxhealth <= 0) return;

        if (_currenLifeText != null) _currenLifeText.text = lifeNum.ToString() + "/" +maxhealth;
        if (_bar_lifeBarFillable != null) _bar_lifeBarFillable.fillAmount = (float)lifeNum / maxhealth;
    }

}

