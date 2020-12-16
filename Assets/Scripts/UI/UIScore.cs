using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIScore : MonoBehaviour
{
    TextMeshProUGUI _scoreText;

    void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UIManager.Instance.OnScoreChange.AddListener(OnScoreChange);
    }
    void OnScoreChange()
    {
        _scoreText.SetText(UIManager.Instance.Score.ToString("D6"));
    }

}
