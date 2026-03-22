using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private TextMeshProUGUI _currenLifeText;
    [SerializeField] private Image _bar_lifeBarFillable;

    private void OnEnable()
    {
        //if (_lifeController == null) _lifeController = FindObjectOfType<LifeController>();

        //Debug.Log("mi attivo e mi registro per onLifechanged !!");
        _lifeController._onHealthChange += UpdateLifeText;

        UpdateLifeText(_lifeController.GetHp(), _lifeController.GetMaxHp()); // forzo lettura e aggiornamento UI del numero di vite attuale
    }

    private void OnDisable()
    {
        _lifeController._onHealthChange -= UpdateLifeText;
    }

    private void UpdateLifeText(int lifeNum, int maxhealth)
    {
        _currenLifeText.text = lifeNum.ToString() + "/" +maxhealth;
       _bar_lifeBarFillable.fillAmount = lifeNum / maxhealth;
    }

}

