using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerType
{
    Small,
    Big
}
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerStateController))]
[RequireComponent(typeof(PlayerCombatController))]
public class PlayerController : MonoBehaviour
{
    #region Type
    PlayerType _type;
    #endregion
    #region Components
    Animator _animator;
    SpriteRenderer _render;
    AudioSource _audio;
    BoxCollider2D _boxCollider;
    Rigidbody2D _rb;
    PlayerInput _playerInput;
    PlayerCombatController _playerCombat;
    #endregion
    #region Movement
    Vector2 _moveVector = Vector2.zero;
    #endregion
    #region Jump
    [Header("Jump")]
    float _jumpSpeed = 15f;
    [SerializeField]
    float _jumpAbortSpeedReduction = 10f;
    [SerializeField]
    float _gravity = 15f;
    [SerializeField]
    float _airAcceleration = 220;
    [SerializeField]
    float _airDeceleration = 200;
    #endregion
    #region Run
    [Header("Run")]
    [SerializeField]
    float _runSpeed = 10f;
    [SerializeField]
    float _groundAcceleration = 100f;
    [SerializeField]
    float _groundDeceleration = 100f;
    #endregion
    #region Dash
    [Header("Dash")]
    [SerializeField]
    float dashTime = 0.3f;
    [SerializeField]
    float dashSpeed = 20f;
    [SerializeField]
    float distanceBetweenImages = 0.25f;
    [SerializeField]
    float dashCooldown = 2.0f;
    [SerializeField]
    float dashAcceleration = 125f;

    float dashTimeLeft;
    float lastImageXPos;
    #endregion
    #region State Variables
    PlayerStateController _playerState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerRunState runState = new PlayerRunState();
    public PlayerShootingState shootingState = new PlayerShootingState();

    string _currentState;
    #endregion
    #region Animation
    [HideInInspector]
    public int boolJumpParameter = Animator.StringToHash("IsJumping");
    [HideInInspector]
    public int boolRunParameter = Animator.StringToHash("IsRunning");
    [HideInInspector]
    public int triggerBigTransformParameter = Animator.StringToHash("IsBigTransform");
    [HideInInspector]
    public int _boolDashParameter = Animator.StringToHash("IsDashing");
    [HideInInspector]
    public int _boolAttackParameter = Animator.StringToHash("IsAttacking");
    [HideInInspector]
    public int boolShootParameter = Animator.StringToHash("IsShooting");
    #endregion
    #region Sound
    [Header("Sound")]
    [SerializeField]
    AudioEvent _powerUpSound;
    #endregion
    #region Collisions
    [Header("Collisions")]
    [Tooltip("Uses for checking ceiling and ground")]
    [SerializeField]
    float _collisionCheckDistance = 0.1f;
    [SerializeField]
    LayerMask collisionLayer;
    #endregion
    #region Combat
    [Header("Melee Attack")]
    [SerializeField]
    Transform _attackHitBox;
    [SerializeField]
    float _attackRadius = 0.5f;
    [SerializeField]
    float _attackDamage = 1.0f;
    [SerializeField]
    LayerMask _damagableLayer;
    bool _isAttacking = false;
    #endregion
    #region Particles
    [Header("Particles")]
    [SerializeField]
    ParticleSystem _dashEffect;
    [SerializeField]
    float _dashEffectVelocity = 10.0f;
    #endregion
    #region Getter/Setter
    public PlayerType Type { get { return _type; } }
    public PlayerInput Input { get { return _playerInput; } }
    public PlayerCombatController Combat { get { return _playerCombat; } }
    public Vector2 MoveVector { get { return _moveVector; } }
    public bool FaceRight { get; private set; }
    public bool IsCeiling { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsBigTransform { get {return _animator.GetBool(triggerBigTransformParameter); }  }
    public bool IsDashing { get; private set; }
    public bool IsAttacking { get { return _isAttacking; } }
    public bool IsShooting { get { return _playerCombat.isShooting; } }
    public string CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    public float JumpSpeed { get { return _jumpSpeed; } }
    #endregion
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _playerState = new PlayerStateController(this);
        _playerInput = GetComponent<PlayerInput>();
        _playerCombat = GetComponent<PlayerCombatController>();
    }
    void Start()
    {
        FaceRight = true;
        _playerState.Initialize(idleState);
    }
    void Update()
    {
        Debug.Log("Current state: " + _currentState);
        _playerState.Update();
    }
    void FixedUpdate()
    {
        _playerState.FixedUpdate();
        Flip();

        Move(_moveVector);
    }
    #region Basic Movement
    public void Move(Vector2 nextMovement)
    {
        _rb.MovePosition(_rb.position + nextMovement * Time.fixedDeltaTime);
    }
    public void AirborneVerticalMovement()
    {
        // Simulate jump action, Slowly decrease up vector toward max height
        if (_moveVector.y > 0)
        {
            _moveVector.y -= _jumpAbortSpeedReduction * Time.fixedDeltaTime;
        }

        // Reset up vector when hit upper obstacle
        if (Mathf.Approximately(_moveVector.y, 0f) || IsCeiling && _moveVector.y > 0f)
        {
            _moveVector.y = 0f;
        }

        _moveVector.y -= _gravity * Time.fixedDeltaTime;
    }
    public void AirborneHorizontalMovement()
    {
        float accelerate = _playerInput.Horizontal.Down ? _airAcceleration : _airDeceleration;
        float desiredSpeed = _playerInput.Horizontal.Value * _runSpeed;
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, desiredSpeed, accelerate * Time.fixedDeltaTime);
    }
    public void GroundHorizontalMovement()
    {
        float desiredSpeed = _playerInput.Horizontal.Value * _runSpeed;
        _moveVector.x = Mathf.MoveTowards(_moveVector.x, desiredSpeed, _groundAcceleration * Time.fixedDeltaTime);
    }
    // Player is still affected by gravity when grounded
    public void GroundVerticalMovement()
    {
        _moveVector.y -= _gravity * Time.fixedDeltaTime;

        if (_moveVector.y < -_gravity * Time.fixedDeltaTime && IsGrounded)
        {
            _moveVector.y = -_gravity * Time.fixedDeltaTime;
        }
    }
    #endregion
    #region Dash
    public void StartDash()
    {
        IsDashing = true;
        dashTimeLeft = dashTime;
        lastImageXPos = _rb.position.x;

        CreateDashEffect();
        SetParameter(_boolDashParameter, true);
    }
    public void FinishDash()
    {
        IsDashing = false;
        SetParameter(_boolDashParameter, false);
    }
    public void Dash()
    {
        if(!IsGrounded)
        {
            FinishDash();
            return;
        }

        if (IsDashing)
        {
            if (dashTimeLeft > 0)
            {
                dashTimeLeft -= Time.fixedDeltaTime;

                if (Mathf.Abs(_rb.position.x - lastImageXPos) > distanceBetweenImages)
                {
                    PlayerImagePool.Instance.ActivatePooledObject();
                    lastImageXPos = _rb.position.x;
                }
            }
            else if (dashTimeLeft <= 0 || !IsGrounded)
            {
                FinishDash();
            }
        }
    }
    public void CreateDashEffect()
    {
        float flipX = _render.flipX ? 1.0f : 0f;
        _dashEffect.GetComponent<ParticleSystemRenderer>().flip = new Vector3(flipX, 0f, 0f);

        var velocityOverTime = _dashEffect.velocityOverLifetime;
        velocityOverTime.xMultiplier = FaceRight ? -_dashEffectVelocity: _dashEffectVelocity;

        _dashEffect.Play();
    }
    #endregion
    #region Combat
    public void MeleeAttack()
    {
        if (_isAttacking)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackHitBox.position, _attackRadius, _damagableLayer);

            foreach (Collider2D collider in colliders)
            {
                Debug.Log("Attack Hit: " + collider.gameObject.name);
                //TODO: implement this
                //IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
                //if(damageable != null)
                //{
                //    damageable.TakeDamage(attackDamage);
                //}
            } 
        }
    }
    public void StartMeleeAttack()
    {
        _isAttacking = true;
        SetParameter(_boolAttackParameter, true);
    }
    // MeleeAttack animation event 
    public void AEFinishMeleeAttack()
    {
        _isAttacking = false;
        SetParameter(_boolAttackParameter, false);
    }
    #endregion
    #region Others
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
        if (!Mathf.Approximately(_moveVector.x, 0f))
        {
            if (_moveVector.x > Single.Epsilon)
            {
                _render.flipX = false;
                FaceRight = true;
            }
            else if (_moveVector.x < Single.Epsilon)
            {
                _render.flipX = true;
                FaceRight = false;
            } 
        }
    }
    public void CheckCeiling()
    {
        Vector3 centerUpper, leftUpper, rightUpper;
        Vector3 yOffset = new Vector3(0f, _boxCollider.size.y * 0.5f, 0f);
        Vector3 xOffset = new Vector3(_boxCollider.size.x * 0.5f, 0f, 0f);

        centerUpper = _boxCollider.bounds.center + yOffset;
        leftUpper = centerUpper - xOffset;
        rightUpper = centerUpper + xOffset;
        Vector3 distance = new Vector3(0f, _collisionCheckDistance, 0f);

        // Avoid overlap with player's box collider
        _boxCollider.enabled = false;
        RaycastHit2D centerHit = Physics2D.Linecast(centerUpper, centerUpper + distance, collisionLayer);
        RaycastHit2D leftHit = Physics2D.Linecast(leftUpper, leftUpper + distance, collisionLayer);
        RaycastHit2D rightHit = Physics2D.Linecast(rightUpper, rightUpper + distance, collisionLayer);
        _boxCollider.enabled = true;

        IsCeiling = (centerHit.collider != null || leftHit.collider != null || rightHit.collider != null);
    }
    public void CheckGround()
    {
        Vector3 centerBottom, leftBottom, rightBottom;
        Vector3 yOffset = new Vector3(0f, _boxCollider.size.y * 0.5f, 0f);
        Vector3 xOffset = new Vector3(_boxCollider.size.x * 0.5f, 0f, 0f);

        centerBottom = _boxCollider.bounds.center - yOffset;
        leftBottom = centerBottom - xOffset;
        rightBottom = centerBottom + xOffset;
        Vector3 distance = new Vector3(0f, _collisionCheckDistance, 0f);

        // Avoid overlap with player's box collider
        _boxCollider.enabled = false;
        RaycastHit2D centerHit = Physics2D.Linecast(centerBottom, centerBottom - distance, collisionLayer);
        RaycastHit2D leftHit = Physics2D.Linecast(leftBottom, leftBottom -distance, collisionLayer);
        RaycastHit2D rightHit = Physics2D.Linecast(rightBottom, rightBottom - distance, collisionLayer);
        _boxCollider.enabled = true;

        IsGrounded = (centerHit.collider != null || leftHit.collider != null || rightHit.collider != null);
    }
    public void BigTransform()
    {
        SetParameter(triggerBigTransformParameter);
        _powerUpSound.Play(_audio);
        // Change box collider size to match with sprite size
        Vector2 spriteSize = _render.sprite.bounds.size;
        _boxCollider.size = spriteSize;
        _boxCollider.offset = new Vector2(0f, 1f);
    }
    public void FireShooter()
    {
        return;
    }
    public void Invincible()
    {
        return;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackHitBox.position, _attackRadius);
    }
    #endregion
}
