using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MarioController : Mario
{
    #region Type
    public MarioType Type
    {
        get { return _type; }
    }
    MarioType _type;
    #endregion

    #region Components
    Rigidbody2D _rb;
    BoxCollider2D _box;
    #endregion

    #region Animation

    #endregion

    #region Movement
    Vector3 _moveVector = Vector3.zero;
    Vector3 _currentPosition;
    public float moveSpeed;

    bool _isJumping = false;
    bool _isGrounded = false;
    #endregion

    public LayerMask layerToCast;
    string COMPONENT_NAME = "[MarioController] ";
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        GroundCheck();
        HandleWalkMovement();
        HandleOnAirMovement();
        Move();
    }

    void GroundCheck()
    {
        Vector2 collidercenter = _box.bounds.center;
        Vector2 colliderSize = _box.size;

        Vector2 boxCenter = new Vector2(collidercenter.x, collidercenter.y - (colliderSize.y / 2));
        Vector2 boxSize = new Vector2(colliderSize.x, colliderSize.y * 0.9f);

        _box.enabled = false;
        _isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0, layerToCast.value) != null;
        
        //Debug.Log(COMPONENT_NAME + "Grounded: " + _isGrounded);
        _box.enabled = true;
    }
    void HandleWalkMovement()
    {
        if (!InputController.Instance.IsGettingInput)
            return;

        _moveVector.x = InputController.Instance.InputVector.x * moveSpeed;
    }

    void HandleOnAirMovement()
    {
        if (!InputController.Instance.IsGettingInput)
            return;

        if (!_isGrounded)
            return;

        _moveVector.y = InputController.Instance.InputVector.y > 0 ? 1f : 0f;
        _moveVector.y = _moveVector.y * moveSpeed;
        _isJumping = true;
    }
    void Move()
    {
        if (!InputController.Instance.IsGettingInput)
            return;

        _currentPosition = _rb.position;
        Vector3 movePosition = _currentPosition + _moveVector * Time.fixedDeltaTime; ;
        //Debug.Log(COMPONENT_NAME + "MoveVector: " + _moveVector);
        _rb.MovePosition(movePosition);

        // Reset move vector
        _moveVector = Vector3.zero;
    }
    protected override void BigTransform()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireShooter()
    {
        throw new System.NotImplementedException();
    }

    protected override void Invincible()
    {
        throw new System.NotImplementedException();
    }
}
