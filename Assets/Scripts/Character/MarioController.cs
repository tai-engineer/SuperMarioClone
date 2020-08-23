using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Mario))]
[RequireComponent(typeof(MarioInput))]
public class MarioController : MonoBehaviour
{
    #region Type
    public MarioType Type
    {
        get { return _type; }
    }
    MarioType _type;
    #endregion

    #region References
    Mario _mario;
    MarioInput _marioInput;
    #endregion

    #region Animation

    #endregion

    #region Movement
    Vector2 _moveVector = Vector2.zero;
    
    #region Jump
    public float jumpSpeed = 20f;
    public float jumpAbortSpeedReduction = 100f;
    public float gravity = 50f;
    #endregion

    #region Run
    public float runSpeed = 10f;
    public float groundAcceleration = 100f;
    public float groundDeceleration = 100f;
    #endregion
    #endregion

    void Awake()
    {
        _mario = GetComponent<Mario>();
        _marioInput = GetComponent<MarioInput>();
    }

    void Update()
    {
        if(_marioInput.Jump.Down)
        {
            _moveVector.y = jumpSpeed;
        }

    }
    void FixedUpdate()
    {
        HandleVerticalMovement();
        HandleHorizontalMovement();

        _mario.Move(_moveVector);
    }

    void HandleVerticalMovement()
    {
        /* Simulate jump action
         * Slowly decrease up vector toward max height
         */
        if (_moveVector.y > 0)
        {
            _moveVector.y -= jumpAbortSpeedReduction * Time.fixedDeltaTime;
        }

        // Reset up vector when hit upper obstacle
        if(Mathf.Approximately(_moveVector.y, 0f) || _mario.IsCeiling && _moveVector.y > 0f)
        {
            _moveVector.y = 0f;
        }

        // Mario is still affected by gravity when grounded
        _moveVector.y -= gravity * Time.fixedDeltaTime;

        if (_moveVector.y < -gravity * Time.fixedDeltaTime && _mario.IsGrounded)
        {
            _moveVector.y = -gravity * Time.fixedDeltaTime;
        }
    }
    void HandleHorizontalMovement()
    {
        float desiredSpeed = _marioInput.Horizontal.Value * runSpeed;
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, desiredSpeed, groundAcceleration * Time.fixedDeltaTime);
    }
    public void BigTransform()
    {
        
    }

    public void FireShooter()
    {
        
    }

    public void Invincible()
    {
        
    }
}
