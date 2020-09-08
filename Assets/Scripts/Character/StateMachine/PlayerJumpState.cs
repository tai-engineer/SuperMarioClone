﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public override void EnterState(PlayerStateController state, PlayerController player)
    {
        Debug.Log("State: Jump");
        player.SetJumpSpeed(player.jumpSpeed);
        player.SetParameter(player.boolJumpParameter, true);
    }

    public override void ExitState(PlayerStateController state, PlayerController player)
    {
        player.SetParameter(player.boolJumpParameter, false);
    }

    public override void FixedUpdate(PlayerStateController state, PlayerController player)
    {
        player.CheckGround();
        player.CheckCeiling();
        player.AirborneVerticalMovement();
        player.AirborneHorizontalMovement();
    }

    public override void Update(PlayerStateController state, PlayerController player)
    {
        if(player.IsGrounded && player.MoveVector.y < 0)
        {
            state.ChangeState(player.idleState);
        }
    }
}
