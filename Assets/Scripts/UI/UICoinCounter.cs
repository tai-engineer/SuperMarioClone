using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UICoinCounter : MonoBehaviour
{
    TextMeshProUGUI _coinCounterText;
    void Awake()
    {
        _coinCounterText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UIManager.Instance.OnCoinCollected.AddListener(OnCoinCollected);
    }
    void OnCoinCollected()
    {
        _coinCounterText.SetText(UIManager.Instance.CoinNumber.ToString("D2"));
    }
}
