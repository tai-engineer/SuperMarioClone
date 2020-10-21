using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    TextMeshProUGUI _scoreText;

    void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _scoreText.SetText(LevelManager.Instance.Score.ToString("D6")); // TODO: use event to update
    }
}
