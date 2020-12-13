using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshPro))]
public class FloatingScore : MonoBehaviour
{
    [SerializeField]
    float _disapearTime = 1.0f;
    [SerializeField]
    float _fadeSpeed = 1.0f;
    [SerializeField]
    float _floatSpeed = 1.0f;

    TextMeshPro _text;
    Color _textColor;


    void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        _textColor = _text.color;
    }
    void Update()
    {
        transform.Translate(0, _floatSpeed * Time.deltaTime, 0);

        _textColor.a -= _fadeSpeed * Time.deltaTime;
        _text.color = _textColor;
        _disapearTime -= Time.deltaTime;
        if(_disapearTime < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
