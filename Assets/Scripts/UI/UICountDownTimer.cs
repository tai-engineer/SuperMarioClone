using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICountDownTimer : MonoBehaviour
{
    TextMeshProUGUI _counter;
    float _timer = 0f;
    int timeLimit = 400;
    void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        int seconds = (int)_timer % 60;
        _counter.SetText((timeLimit - seconds).ToString());
    }
}
