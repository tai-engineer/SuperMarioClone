using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void EnterState(PlayerStateController state, PlayerController player)
    {
        player.CurrentState = "Idle";
    }

    public override void ExitState(PlayerStateController state, PlayerController player)
    {

    }

    public override void FixedUpdate(PlayerStateController state, PlayerController player)
    {
        player.CheckGround();
        if (player.IsGrounded)
        {
            player.SetJumpSpeed(0f);
        }
        player.GroundVerticalMovement();
        player.GroundHorizontalMovement();
        player.MeleeAttack();
    }

    public override void Update(PlayerStateController state, PlayerController player)
    {
        if (player.IsGrounded && player.Input.Jump.Down)
        {
            state.ChangeState(player.jumpState);
        }
        else if(player.IsGrounded && player.Input.Horizontal.Down)
        {
            state.ChangeState(player.runState);
        }
        else if (player.Input.Shooting.Down)
        {
            state.ChangeState(player.shootingState);
        }
        else if (player.Input.MeleeAttack.Down)
        {
            player.StartMeleeAttack();
        }
    }
}
