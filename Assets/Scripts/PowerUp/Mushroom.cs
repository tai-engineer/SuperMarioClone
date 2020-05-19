using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Mushroom: MonoBehaviour
{
    public enum PowerUpType
    {
        SUPER
    };

    public PowerUpType Type {get; protected set;}

    #region Components
    protected Rigidbody2D _rb;
    protected AudioSource _audio;
    protected SpriteRenderer _sprite;
    protected CircleCollider2D _collider;
    #endregion

    #region Events
    public event EventsManager.MushroomCollisionEvent MushroomCollision;
    #endregion

    #region Mushroom Variables
    // Audio
    public AudioClip AppearanceClip;

    // Movement
    Vector3 _moveVector = Vector3.zero;
    Vector3 _dir = Vector3.right;
    public float moveDistance = 1.0f;
    [Range(0.0f, 100.0f)]
    public float speed = 1.0f;

    // Functionality
    public bool patrol = true;
    public bool appearMove = true;
    public float gravity = 1;

    Vector3 _startPos;
    #endregion
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    protected virtual void Start()
    {
        _collider.enabled = false;
        _rb.gravityScale = 0f;
        _startPos = _rb.position;

        _audio.PlayOneShot(AppearanceClip);
    }
    // Move object in 2D space, use this in FixedUpdate
    protected virtual void Patrol()
    {
        if (patrol && !appearMove)
        {
            _moveVector = _rb.position;
            _moveVector += _dir * moveDistance * Time.fixedDeltaTime * speed;
            _rb.MovePosition(_moveVector);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("[Mushroom] Mushroom collided.");
        // Raise an event then get destroy by player
        if (collision.gameObject.CompareTag("Mario"))
        {
            MushroomCollision(Type);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Pipe"))
        {
            _dir *= -1.0f;
        }
    }

    // Move Mushroom only once in upward direction after being instantiated
    protected void MoveUpward(float distance)
    {
        if(appearMove)
        {
            Vector3 up = Vector3.up * distance;
            Vector3 target = Vector3.MoveTowards(_rb.position, _startPos + up, Time.fixedDeltaTime * speed * 0.5f);
            _rb.MovePosition(target);

            if (Vector3.Distance(_rb.position, target) < 0.001f)
            {
                appearMove = false;
                _collider.enabled = true;
                _rb.gravityScale = gravity;
            }
        }
    }
}
