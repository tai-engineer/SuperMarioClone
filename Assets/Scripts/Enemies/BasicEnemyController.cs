using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BasicEnemyController : MonoBehaviour, IDamageable, IScore
{
    #region References
    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    Animator _anim;
    #endregion

    #region Private Variables
    [SerializeField]
    Transform wallCheckPoint;
    [SerializeField]
    float wallCheckDistance = 0.2f;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    AudioClip _deadClip;
    [SerializeField]
    GameObject _scorePopUp;
    [SerializeField]
    BasicEnemy_SO _enemyDefinition;

    int _faceDirection = 1;
    bool _isWallDetected = false;
    bool _isDead = false;
    Vector2 _moveVector;
    #endregion

    public BasicEnemy_SO EnemyDefinition { get { return _enemyDefinition; } }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioPlayer = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(_isDead)
        {
            return;
        }

        WallCheck();
        if (_isWallDetected)
        {
            Flip();
        }
        Patrol();
    }
    void Patrol()
    {
         _moveVector = new Vector3(_enemyDefinition.speed * _faceDirection * Time.fixedDeltaTime, 0f);
        _rb.MovePosition(_rb.position + _moveVector);
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
            _isDead = true;
            AddScore(_enemyDefinition.score);
            PopScore();
            Die();
        }
    }

    void Die()
    {
        _audioPlayer.PlayOneShot(_deadClip);
        _anim.SetBool("IsDead", _isDead);

        _moveVector = Vector2.zero;

        

        Destroy(gameObject, _deadClip.length);
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
    void PopScore()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.3f, 0f);
        UIManager.Instance.PopScore(position, _enemyDefinition.score);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(wallCheckPoint.position, wallCheckPoint.position + new Vector3(wallCheckDistance, 0f, 0f));
    }

    public void AddScore(int score)
    {
        GameManager.Instance.AddScore(score);
    }
}
