using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    LEFT = -1,
    RIGHT = 1
}
public class InputController : Singleton<InputController>

{
    Vector3 _inputVector = Vector3.zero;
    MoveDirection _dir = MoveDirection.RIGHT;
    MoveDirection _prevDir = MoveDirection.RIGHT;
    bool _dirChanged = false;
    public bool IsDirChanged { get; private set; }
    public bool IsGettingInput { get; private set; }
    public Vector3 InputVector { get { return _inputVector; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = Input.GetAxisRaw("Vertical");
        //Debug.Log("[InputController] _inputVector.x: " + _inputVector.x);
        //Debug.Log("[InputController] _inputVector.y: " + _inputVector.y);

        _dir = _inputVector.x > 0 ? MoveDirection.RIGHT : MoveDirection.LEFT;
        IsDirChanged = (_dir != _prevDir);
        _prevDir = _dir;

        IsGettingInput = (_inputVector.x != 0f || _inputVector.y != 0f) ? true: false;
    }

    public MoveDirection GetDirection()
    {
        return _dir;
    }
}
