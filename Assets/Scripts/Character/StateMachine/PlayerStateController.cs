using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController
{
    public PlayerState CurrentState { get; protected set; }

    PlayerController _player;

    public PlayerStateController(PlayerController mario)
    {
        _player = mario;
    }

    public void Initialize(PlayerState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this, _player);
    }

    public void ChangeState(PlayerState state)
    {
        CurrentState.ExitState(this, _player);
        CurrentState = state;
        CurrentState.EnterState(this, _player);
    }

    public void Update()
    {
        CurrentState.Update(this, _player);
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdate(this, _player);
    }
}
