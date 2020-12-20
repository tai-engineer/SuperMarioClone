using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class BasicEnemyController : MonoBehaviour, IDamageable, IScore
{
    #region References
    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    Animator _anim;
    BoxCollider2D _box;
    #endregion

    #region Audio
    [SerializeField]
    AudioClip _deadClip;
    #endregion

    #region Enemy Definition
    [SerializeField]
    BasicEnemy_SO _enemyDefinition;
    #endregion

    #region Physics
    float _distanceToGnd = 0.1f;
    float _distanceBtwPoints = 0.45f;
    float _distanceToObs = 0.1f;

    bool _isWallDetected = false;
    bool _isDead = false;
    bool _isGrounded = false;

    [SerializeField]
    LayerMask _layerToCheck;
    int _faceDirection = 1;
    Vector2 _moveVector;
    #endregion
    [SerializeField]
    bool _debugDraw = false;

    public BasicEnemy_SO EnemyDefinition { get { return _enemyDefinition; } }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioPlayer = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        if(_isDead)
        {
            return;
        }

        WallCheck();
        GroundCheck();
        Patrol();
    }
    void Patrol()
    {
        if (_isWallDetected)
        {
            Flip();
        }

        _moveVector = new Vector2(_enemyDefinition.speed * _faceDirection * Time.fixedDeltaTime, 0f);

        if(!_isGrounded)
        {
            Stop();
        }
        _rb.MovePosition(_rb.position + _moveVector);
    }
    void Stop()
    {
        _moveVector = Vector2.zero;
    }
    public void TakeDamage(int damgage)
    {
        if (_isDead)
        {
            return;
        }

        _enemyDefinition.health = _enemyDefinition.health < 0 ? 0 : _enemyDefinition.health - damgage;
        if(_enemyDefinition.health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        _isDead = true;

        _audioPlayer.PlayOneShot(_deadClip);
        _anim.SetBool("IsDead", _isDead);

        Stop();
        AddScore(_enemyDefinition.score);
        PopScore();

        Destroy(gameObject, _deadClip.length);
    }
    void Flip()
    {
        _faceDirection *= -1;
    }
    void WallCheck()
    {
        Vector2 pos = new Vector2(_box.bounds.center.x, _box.bounds.center.y);
        pos.x = _faceDirection < 0 ? pos.x - _box.size.x / 2 : pos.x + _box.size.x / 2;
        float offset = 0.075f;

        _isWallDetected = PhysicUtil.ObstacleCheck(pos, _faceDirection < 0, _distanceToObs, _layerToCheck, offset, _debugDraw);
    }
    void GroundCheck()
    {
        float offset = 0.01f;
        Vector2 _groundPosition = new Vector2(_box.bounds.center.x, _box.bounds.center.y - _box.size.y / 2);

        _isGrounded = PhysicUtil.GroundCheck(_groundPosition, _distanceBtwPoints, _distanceToGnd, _layerToCheck, offset, _debugDraw);
    }
    void PopScore()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.3f, 0f);
        UIManager.Instance.PopScore(position, _enemyDefinition.score);
    }
    public void AddScore(int score)
    {
        GameManager.Instance.AddScore(score);
    }
}
