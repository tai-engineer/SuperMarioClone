using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mario))]
[RequireComponent(typeof(MarioInput))]
[RequireComponent(typeof(MarioStateController))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]

public class MarioController : MonoBehaviour
{
    #region Type
    MarioType _type;
    #endregion

    #region References
    Mario _mario;
    MarioInput _marioInput;
    Animator _animator;
    SpriteRenderer _render;
    AudioSource _audio;
    #endregion

    #region Movement
    Vector2 _moveVector = Vector2.zero;

    #region Jump
    public float jumpSpeed = 20f;
    public float jumpAbortSpeedReduction = 100f;
    public float gravity = 50f;
    public float airAcceleration = 100f;
    public float airDeceleration = 100f;
    #endregion

    #region Run
    public float runSpeed = 10f;
    public float groundAcceleration = 100f;
    public float groundDeceleration = 100f;
    #endregion
    #endregion

    #region State Variables
    MarioStateController _marioState;

    public MarioIdleState idleState = new MarioIdleState();
    public MarioJumpState jumpState = new MarioJumpState();
    public MarioRunState runState = new MarioRunState();
    #endregion

    #region Animation
    [HideInInspector]
    public int boolJumpParameter = Animator.StringToHash("IsJumping");
    [HideInInspector]
    public int boolRunParameter = Animator.StringToHash("IsRunning");
    [HideInInspector]
    public int triggerBigTransformParameter = Animator.StringToHash("IsBigTransform");

    #region Getter/Setter
    public MarioType Type { get { return _type; } }
    public MarioInput Input { get { return _marioInput; } }
    public Vector2 MoveVector { get { return _moveVector; } }
    public bool IsGrounded { get { return _mario.IsGrounded; } }
    public bool IsCeiling { get { return _mario.IsCeiling; } }
    public bool IsBigTransform { get {return _animator.GetBool(triggerBigTransformParameter); }  }
    #endregion
    #endregion
    #region Audio
    public AudioEvent powerUpSound;
    #endregion
    void Awake()
    {
        _mario = GetComponent<Mario>();
        _marioInput = GetComponent<MarioInput>();
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();

        _marioState = new MarioStateController(this);
    }

    void Start()
    {
        _marioState.Initialize(idleState);
        
    }
    void Update()
    {
        _marioState.Update();
    }

    void FixedUpdate()
    {
        _marioState.FixedUpdate();
        Flip();

        _mario.Move(_moveVector);
    }

    public void AirborneVerticalMovement()
    {
        // Simulate jump action, Slowly decrease up vector toward max height
        if (_moveVector.y > 0)
        {
            _moveVector.y -= jumpAbortSpeedReduction * Time.fixedDeltaTime;
        }

        // Reset up vector when hit upper obstacle
        if (Mathf.Approximately(_moveVector.y, 0f) || _mario.IsCeiling && _moveVector.y > 0f)
        {
            _moveVector.y = 0f;
        }

        _moveVector.y -= gravity * Time.fixedDeltaTime;
    }

    public void AirborneHorizontalMovement()
    {
        float accelerate = _marioInput.Horizontal.Down ? airAcceleration : airDeceleration;
        float desiredSpeed = _marioInput.Horizontal.Value * runSpeed;
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, desiredSpeed, accelerate * Time.fixedDeltaTime);
    }
    public void GroundHorizontalMovement()
    {
        float desiredSpeed = _marioInput.Horizontal.Value * runSpeed;
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, desiredSpeed, groundAcceleration * Time.fixedDeltaTime);
    }
    // Mario is still affected by gravity when grounded
    public void GroundVericalMovement()
    {
        _moveVector.y -= gravity * Time.fixedDeltaTime;

        if (_moveVector.y < -gravity * Time.fixedDeltaTime)
        {
            _moveVector.y = -gravity * Time.fixedDeltaTime;
        }
    }
    public void SetJumpSpeed(float speed)
    {
        _moveVector.y = speed;
    }
    public void SetParameter(int hashParameter, bool value)
    {
        _animator.SetBool(hashParameter, value);
    }
    public void SetParameter(int hashParameter)
    {
        _animator.SetTrigger(hashParameter);
    }
    public void Flip()
    {
        if (_moveVector.x > 0)
        {
            _render.flipX = false;
        }
        else if(_moveVector.x < 0)
        {
            _render.flipX = true;
        }
    }
    public void BigTransform()
    {
        SetParameter(triggerBigTransformParameter);
        powerUpSound.Play(_audio);
        // Change box collider size to match with sprite size
        Vector2 spriteSize = _render.sprite.bounds.size;
        _mario.boxCollider.size = spriteSize;
        _mario.boxCollider.offset = new Vector2(0f, 1f);
    }

    public void FireShooter()
    {
        return;
    }

    public void Invincible()
    {
        return;
    }
}
