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
        UIManager.Instance.ScoreUpdateEvent.AddListener(OnScoreUpdate);
    }
    void OnScoreUpdate()
    {
        _scoreText.SetText(UIManager.Instance.Score.ToString("D6"));
    }

}
