using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioStateController
{
    public MarioState CurrentState { get; protected set; }

    MarioController _mario;

    public MarioStateController(MarioController mario)
    {
        _mario = mario;
    }

    public void Initialize(MarioState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this, _mario);
    }

    public void ChangeState(MarioState state)
    {
        CurrentState.ExitState(this, _mario);
        CurrentState = state;
        CurrentState.EnterState(this, _mario);
    }

    public void Update()
    {
        CurrentState.Update(this, _mario);
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdate(this, _mario);
    }
}
