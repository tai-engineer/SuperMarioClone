using UnityEngine;
using System;

public class PlayerInput: InputController
{
    public InputButton Jump = new InputButton(KeyCode.Space);
    public InputButton Dash = new InputButton(KeyCode.LeftShift);
    public InputAxis Horizontal = new InputAxis(KeyCode.LeftArrow, KeyCode.RightArrow);

    void Update()
    {
        Jump.GetInput();
        Horizontal.GetInput();
        Dash.GetInput();
    }
}
