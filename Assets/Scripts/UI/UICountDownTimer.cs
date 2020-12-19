using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UICountDownTimer : MonoBehaviour
{
    TextMeshProUGUI _counter;
    float _timer = 0f;
    int _timeLimit = 400;
    void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if(_timeLimit <= 0)
        {
            UIManager.Instance.CountDownEndEvent.Invoke();
            return;
        }

        _timer += Time.deltaTime;
        if(_timer > 1)
        {
            _timeLimit -= (int)_timer;
            _counter.SetText(_timeLimit.ToString());
            _timer = 0f;
        }
    }
}
