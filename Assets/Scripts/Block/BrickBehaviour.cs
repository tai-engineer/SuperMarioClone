using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BrickBehaviour : MonoBehaviour
{
    Vector3 _shatterVector;
    float _shatterForce = 1.0f;
    bool _startShatter = false;
    Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_startShatter)
        {
            Shatter();
            StopShatter();
        }
    }

    /* Get destroy when moving out of camera view */
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void SetForce(Vector3 force)
    {
        _shatterVector = force;
    }

    public void StartShatter()
    {
        _startShatter = true;
    }
    public void StopShatter()
    {
        _startShatter = false;
    }
    void Shatter()
    {
        _rb.AddForce(_shatterVector * _shatterForce, ForceMode2D.Impulse);
    }
}
