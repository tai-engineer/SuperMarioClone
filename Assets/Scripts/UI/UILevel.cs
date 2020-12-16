using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UILevel : MonoBehaviour
{
    TextMeshProUGUI _textLevel;

    void Awake()
    {
        _textLevel = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UIManager.Instance.OnNextLevel.AddListener(OnNextLevel);
    }
    void OnNextLevel()
    {
        _textLevel.SetText(UIManager.Instance.CurrentLevel);
    }
}
