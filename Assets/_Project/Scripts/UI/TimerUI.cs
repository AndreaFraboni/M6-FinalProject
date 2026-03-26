using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currenTimeText;

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += UpdateTimerText;
        }
        else
        {
            Debug.LogError("Timemanager: is NULL ?????");
        }

    }

    private void OnDisable()
    {
        if (TimeManager.Instance) TimeManager.Instance.OnTimeChanged -= UpdateTimerText;
    }

    private void UpdateTimerText(float time)
    {
        if (time <= 0) return;
        if (_currenTimeText != null) _currenTimeText.text = $"{(int)time} s";
    }

}
