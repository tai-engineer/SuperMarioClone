using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioJumpState : MarioState
{
    public override void EnterState(MarioStateController state, MarioController player)
    {
        Debug.Log("State: Jump");
        player.SetJumpSpeed(player.jumpSpeed);
        player.SetParameter(player.boolJumpParameter, true);
    }

    public override void ExitState(MarioStateController state, MarioController player)
    {
        player.SetParameter(player.boolJumpParameter, false);
    }

    public override void FixedUpdate(MarioStateController state, MarioController player)
    {
        player.AirborneVerticalMovement();
        player.AirborneHorizontalMovement();
    }

    public override void Update(MarioStateController state, MarioController player)
    {
        if(player.IsGrounded && player.MoveVector.y < 0)
        {
            state.ChangeState(player.idleState);
        }
    }
}
