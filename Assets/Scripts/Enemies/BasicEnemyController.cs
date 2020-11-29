using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BasicEnemyController : MonoBehaviour, IDamageable
{
    #region References
    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    Animator _anim;
    #endregion

    #region Private Variables
    [SerializeField]
    int health = 1;
    [SerializeField]
    float speed = 4.0f;
    [SerializeField]
    Transform wallCheckPoint;
    [SerializeField]
    float wallCheckDistance = 0.2f;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    AudioClip _deadClip;

    int _faceDirection = 1;
    bool _isWallDetected = false;
    bool _isDead = false;
    Vector2 _moveVector;
    #endregion

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioPlayer = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        WallCheck();
        if (_isWallDetected)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Patrol();
        Die();

        _rb.MovePosition(_rb.position + _moveVector);
    }
    void Patrol()
    {
         _moveVector = new Vector3(speed * _faceDirection * Time.fixedDeltaTime, 0f);
    }

    public void TakeDamage(int damgage)
    {
        if (_isDead)
        {
            return;
        }

        health = health < 0 ? 0 : health - damgage;
        if(health <= 0)
        {
            _isDead = true;
            _audioPlayer.PlayOneShot(_deadClip);
            _anim.SetBool("IsDead", _isDead);
        }
    }

    void Die()
    {
        if (_isDead)
        {
            _moveVector = Vector2.zero;
            Destroy(gameObject, _deadClip.length);
        }
    }
    void Flip()
    {
        _faceDirection *= -1;
        wallCheckPoint.localPosition = new Vector3(-wallCheckPoint.localPosition.x, 0f, 0f);
        _isWallDetected = false;
    }
    void WallCheck()
    {
        RaycastHit2D hit =  Physics2D.Raycast(wallCheckPoint.position, new Vector3(_faceDirection, 0f, 0f), wallCheckDistance, layer);
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Pipe"))
        {
            _isWallDetected = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(wallCheckPoint.position, wallCheckPoint.position + new Vector3(wallCheckDistance, 0f, 0f));
    }
}
