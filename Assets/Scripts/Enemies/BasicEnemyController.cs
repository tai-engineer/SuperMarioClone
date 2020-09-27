using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BasicEnemyController : MonoBehaviour
{
    public float health = 1f;
    public float speed = 10f;
    public Transform head;
    public float headCheckRadius;
    public Transform wallCheckPoint;
    public float wallCheckDistance = 0.2f;
    public LayerMask layer;
    int _faceDirection = 1;
    bool _isWallDetected = false;
    bool _isStomped = false;
    bool _isDead = false;

    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    Animator _anim;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioPlayer = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        WallCheck();
        HeadCheck();
        if (_isWallDetected)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Patrol();
    }
    void Patrol()
    {
        Vector2 moveVector = new Vector3(speed * _faceDirection * Time.fixedDeltaTime, 0f);
        _rb.MovePosition(_rb.position + moveVector);
    }

    public void TakeDamage(float damgage)
    {
        if (_isDead)
        {
            return;
        }

        health = health < 0f ? 0f : health - damgage;
        if(health <= 0)
        {
            Die();
        }
        _isStomped = false;
    }

    void Die()
    {
        _isDead = true;
        _audioPlayer.Play();
        _anim.SetBool("IsDead", _isDead);
        Destroy(gameObject, 0.5f);
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

    void HeadCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(head.position, headCheckRadius, layer);
        if (collider != null && collider.gameObject.CompareTag("Player"))
        {
            _isStomped = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(wallCheckPoint.position, wallCheckPoint.position + new Vector3(wallCheckDistance, 0f, 0f));
    }
}
