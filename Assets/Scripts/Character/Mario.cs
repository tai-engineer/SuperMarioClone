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

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public Rigidbody2D rigidBody2D { get { return rb; } }

    Vector2 _nextMovement = Vector2.zero;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _nextMovement * Time.fixedDeltaTime);
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
        Vector3 offset = new Vector3(0f, boxCollider.size.y * 0.5f, 0f);

        point = bottom ? boxCollider.bounds.center - offset : boxCollider.bounds.center + offset;
        radius = boxCollider.size.x * 0.1f;

        // Avoid overlap with mario's box collider
        boxCollider.enabled = false;
        Collider2D collider = Physics2D.OverlapCircle(point, radius);
        boxCollider.enabled = true;

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
