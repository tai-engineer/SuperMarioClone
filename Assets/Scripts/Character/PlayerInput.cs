using UnityEngine;
using System;

public class PlayerInput: InputController
{
    public InputButton Jump = new InputButton(KeyCode.Space);
    public InputAxis Horizontal = new InputAxis(KeyCode.LeftArrow, KeyCode.RightArrow);

    void Update()
    {
        Jump.GetInput();
        Horizontal.GetInput();
    }
}
