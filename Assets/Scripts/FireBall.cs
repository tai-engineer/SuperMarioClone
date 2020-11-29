using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 3f)]
    float _speed = 1.0f;
    [SerializeField]
    float _lifeTime = 0.5f;
    [SerializeField]
    int _damage = 1;

    [SerializeField]
    GameObject destroyEffect = null;

    float _maxDistance = 5.0f;
    float _lifeTimeLeft;
    bool hit = false;

    Rigidbody2D _rb;
    Vector2 _moveVector = Vector2.zero;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _lifeTimeLeft = _lifeTime;
    }

    void FixedUpdate()
    {
        //TODO: Change to use rigid body movement
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, transform.right.x * _maxDistance, _speed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + _moveVector);

        _lifeTimeLeft -= Time.fixedDeltaTime;
        DestroyFireBall();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid self hit
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Self-hit!");
            return;
        }

        hit = true;
        Debug.Log("Hit " + collision.gameObject.name);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log("FireBall hits " + collision.gameObject.name + " with " + _damage + " damage");
            damageable.TakeDamage(_damage);
        }
    }

    void DestroyFireBall()
    {
        if (hit || _lifeTimeLeft < 0f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
