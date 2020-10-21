using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevel : MonoBehaviour
{
    TextMeshProUGUI _textLevel;

    void Awake()
    {
        _textLevel = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _textLevel.SetText(LevelManager.Instance.MainLevel.ToString() + " - " + LevelManager.Instance.SubLevel.ToString()); // TODO: use event to update
    }
}
