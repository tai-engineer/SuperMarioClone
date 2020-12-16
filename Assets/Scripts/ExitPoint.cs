using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    bool _isTriggered = false;
    bool _nextLevel = false;
    void Update()
    {
        if(_isTriggered)
        {
            GameManager.Instance.NextLevel();
            _nextLevel = true;
            _isTriggered = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_nextLevel)
        {
            _isTriggered = true; 
        }
    }
}
