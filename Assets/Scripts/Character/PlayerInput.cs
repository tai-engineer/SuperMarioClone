using UnityEngine;
using System;

public class PlayerInput: InputController
{
    public InputButton Jump = new InputButton(KeyCode.Space);
    public InputButton Dash = new InputButton(KeyCode.LeftShift);
    public InputButton MeleeAttack = new InputButton(KeyCode.H);
    public InputButton Shooting = new InputButton(KeyCode.J);

    public InputAxis Horizontal = new InputAxis(KeyCode.A, KeyCode.D);

    void Update()
    {
        Jump.GetInput();
        Horizontal.GetInput();
        Dash.GetInput();
        MeleeAttack.GetInput();
        Shooting.GetInput();
    }
}
