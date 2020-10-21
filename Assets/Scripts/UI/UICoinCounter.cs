using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICoinCounter : MonoBehaviour
{
    TextMeshProUGUI _coinCounterText;
    void Awake()
    {
        _coinCounterText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _coinCounterText.SetText(LevelManager.Instance.CoinNumber.ToString("D2")); // TODO: use event to update
    }
}
