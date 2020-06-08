using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp: MonoBehaviour
{
    #region Components
    protected Rigidbody2D _rb;
    protected AudioSource _audio;
    protected SpriteRenderer _sprite;
    protected CircleCollider2D _collider;
    #endregion

    #region Mushroom Variables
    // Audio
    public AudioClip appearSound;

    // Movement
    public PowerUpMovement movement;
    public bool moveable = false;

    Vector3 _startPos;
    #endregion
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        _collider.enabled = false;
        _rb.gravityScale = 0f;
        _startPos = _rb.position;

        _audio.PlayOneShot(appearSound);
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
        Debug.Log("[Mushroom] Mushroom collided.");
        // Raise an event then get destroy by player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Pipe"))
        {
            movement.direction *= -1.0f;
        }
    }
    public void Apply(GameObject obj)
    {
        //var character = obj.GetComponent<CharacterController>();
        //character.BigTransform();
        Destroy(gameObject);
    }
}
