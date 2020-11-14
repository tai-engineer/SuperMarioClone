using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    float _activeTime = 0.1f;
    float _timeActivated;
    float _alpha;
    [SerializeField]
    float _alphaSet = 0.8f;
    float _alphaMultiplier = 0.85f;


    Transform _player;
    SpriteRenderer _playerRenderrer;
    SpriteRenderer _renderrer;

    Color _color;


    void OnEnable()
    {
        _renderrer = GetComponent<SpriteRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerRenderrer = _player.GetComponent<SpriteRenderer>();

        _alpha = _alphaSet;
        _renderrer.sprite = _playerRenderrer.sprite;
        _renderrer.flipX = _playerRenderrer.flipX;
        transform.position = _player.position;
        transform.rotation = _player.rotation;

        _alpha *= _alphaMultiplier;
        _color = new Color(1f, 1f, 1f, _alpha);
        _renderrer.color = _color;

        _timeActivated = Time.time;
    }

    void Update()
    {

        if(Time.time >= _timeActivated + _activeTime)
        {
            PlayerImagePool.Instance.AddPool(gameObject);
        }
    }
}
