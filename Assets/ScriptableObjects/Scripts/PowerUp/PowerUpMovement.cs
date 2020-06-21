using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Movement", menuName ="Movement/PowerUp")]
public class PowerUpMovement : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public float speed;

    public float gravity;

    [HideInInspector]
    public Vector3 direction = Vector3.right;

    public float MoveUpDistance;
    public float JumpForce;
    readonly Vector3 _jumpVector = new Vector3(0.2f, 0.8f, 0f);
    public LayerMask GroundLayer;
    bool _isMoveUp = false;
    bool _isBouncing = false;
    public void Patrol(MonoBehaviour monoBehaviour)
    {
        if (_isMoveUp)
        {
            var rb = monoBehaviour.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = gravity;
            Vector3 _moveVector = rb.position;
            _moveVector += direction * Time.fixedDeltaTime * speed;
            rb.MovePosition(_moveVector);
        }
    }

    public IEnumerator MoveUpward(MonoBehaviour monoBehaviour)
    {
        var rb = monoBehaviour.gameObject.GetComponent<Rigidbody2D>();
        Vector3 startPos = rb.position;
        Vector3 upVector = Vector3.up * MoveUpDistance;
        Vector3 endPos = startPos + upVector;

        while(Vector3.Distance(rb.position, endPos) > 0.001f)
        {
            Vector3 des = Vector3.MoveTowards(rb.position, endPos, Time.deltaTime * speed);
            rb.MovePosition(des);
            yield return null;
        }

        _isMoveUp = true;
        monoBehaviour.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return null;
    }

    void OnEnable()
    {
        _isMoveUp = false;
    }

    bool GroundCheck(MonoBehaviour monoBehaviour)
    {
        CircleCollider2D collider = monoBehaviour.gameObject.GetComponent<CircleCollider2D>();
        collider.enabled = false;

        float distToGround = collider.radius;
        Vector3 currentPos = monoBehaviour.gameObject.GetComponent<Rigidbody2D>().position;
        Vector3 direction = Vector3.down;

        RaycastHit2D hit = Physics2D.Raycast(currentPos, direction, distToGround + 0.01f, GroundLayer);
        collider.enabled = true;

        if(hit.collider != null)
        {
            _isBouncing = !_isBouncing;
            return true;
        }

        return false;
    }

    public void Bounce(MonoBehaviour monoBehaviour)
    {
        if (_isMoveUp)
        {
            Rigidbody2D rb = monoBehaviour.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = gravity;
            if (GroundCheck(monoBehaviour) && _isBouncing == false)
            {
                rb.AddForce(_jumpVector * JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
