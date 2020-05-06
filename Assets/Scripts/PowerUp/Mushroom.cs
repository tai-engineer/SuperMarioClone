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
    #endregion

    #region Events
    public event EventsManager.MushroomCollisionEvent MushroomCollision;
    #endregion
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    protected abstract void Patrol();
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mario"))
        {
            MushroomCollision(Type);
            Destroy(gameObject);
        }
    }
}
