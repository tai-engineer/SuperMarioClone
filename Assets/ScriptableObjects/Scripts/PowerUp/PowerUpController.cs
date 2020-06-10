using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PowerUp Movement", menuName ="Movement/PowerUp")]
public class PowerUpController : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public float speed;

    public float gravity;

    [HideInInspector]
    public Vector3 direction = Vector3.right;

    public float MoveUpDistance;
    bool _isMoveUp;
    public void Patrol(MonoBehaviour monoBehaviour)
    {
        if (_isMoveUp)
        {
            var rb2D = monoBehaviour.gameObject.GetComponent<Rigidbody2D>();
            Vector3 _moveVector = rb2D.position;
            _moveVector += direction * Time.fixedDeltaTime * speed;
            rb2D.MovePosition(_moveVector); 
        }
    }

    public IEnumerator MoveUpward(MonoBehaviour monoBehaviour)
    {
        var rb2D = monoBehaviour.gameObject.GetComponent<Rigidbody2D>();
        Vector3 startPos = rb2D.position;
        Vector3 upVector = Vector3.up * MoveUpDistance;
        Vector3 endPos = startPos + upVector;

        while(Vector3.Distance(rb2D.position, endPos) > 0.001f)
        {
            Vector3 des = Vector3.MoveTowards(rb2D.position, endPos, Time.deltaTime * speed);
            rb2D.MovePosition(des);
            yield return null;
        }

        _isMoveUp = true;
        monoBehaviour.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        rb2D.gravityScale = gravity;
        yield return null;
    }

    void OnEnable()
    {
        _isMoveUp = false;
    }
}
