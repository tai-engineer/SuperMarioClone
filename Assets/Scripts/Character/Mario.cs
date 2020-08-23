using UnityEngine;

public enum MarioType
{
    Small,
    Big
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Mario : MonoBehaviour
{
    
    public bool IsCeiling { get; protected set; }
    public bool IsGrounded { get; protected set; }

    BoxCollider2D _boxCollider;
    Rigidbody2D _rb;

    public Rigidbody2D rigidBody2D { get { return _rb; } }

    Vector2 _nextMovement = Vector2.zero;
    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _nextMovement * Time.fixedDeltaTime);
        _nextMovement = Vector2.zero;

        CheckCollision();
        CheckCollision(false);
    }

    public void Move(Vector2 moveVector)
    {
        _nextMovement = moveVector;
    }
    void CheckCollision(bool bottom = true)
    {
        Vector2 point;
        float radius;
        Vector2 offset = new Vector2(0f, _boxCollider.size.y * 0.5f);

        point = bottom ? _rb.position - offset : _rb.position + offset;
        radius = _boxCollider.size.x * 0.5f;

        // Avoid overlap with mario's box collider
        _boxCollider.enabled = false;
        Collider2D collider = Physics2D.OverlapCircle(point, radius);
        _boxCollider.enabled = true;

        if (bottom)
        {
            IsGrounded = collider != null; 
        }
        else
        {
            IsCeiling = collider != null;
        }
    }
}
