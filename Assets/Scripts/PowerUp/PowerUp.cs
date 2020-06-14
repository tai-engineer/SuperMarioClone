using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Default,
    SuperMushroom,
    FireFlower,
    Star
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp: MonoBehaviour
{
    #region Components
    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    SpriteRenderer _sprite;
    CircleCollider2D _collider;
    public Animator _animation;
    #endregion

    #region Mushroom Variables
    // PowerUp type
    public PowerUpType type = PowerUpType.Default;
    // Audio
    public AudioEvent appearSound;

    // Movement
    public PowerUpController movement;
    public bool moveable = false;
    Vector3 _startPos;
    #endregion
    void Awake()
    {
        Debug.Assert(type != PowerUpType.Default, "[PowerUp] Select type for PowerUp object.");

        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _audioPlayer = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _collider.enabled = false;
        _rb.gravityScale = 0f;
        _startPos = _rb.position;

        appearSound.Play(_audioPlayer);
        StartCoroutine(movement.MoveUpward(this));
    }

    void FixedUpdate()
    {
        if (moveable)
        {
            movement.Patrol(this);
        }
    }
    // Move object in 2D space, use this in FixedUpdate
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Raise an event then get destroy by player
        if (collision.gameObject.CompareTag("Player"))
        {
            Apply(collision, type);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Pipe"))
        {
            movement.direction *= -1.0f;
        }
    }
    void Apply(Collision2D collision, PowerUpType type)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();
        switch(type)
        {
            case PowerUpType.SuperMushroom:
                //player.BigTransform();
                break;
            case PowerUpType.FireFlower:
                //player.FireShooter();
                break;
            case PowerUpType.Star:
                //player.Invicible();
                break;
        }
        Destroy(gameObject);
    }
}
