using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public override void EnterState(PlayerStateController state, PlayerController player)
    {
        Debug.Log("State: Run");
        player.SetParameter(player.boolRunParameter, true);
    }

    public override void ExitState(PlayerStateController state, PlayerController player)
    {
        player.SetParameter(player.boolRunParameter, false);
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
    }
    public override void Update(PlayerStateController state, PlayerController player)
    {
        if (player.Input.Jump.Down)
        {
            state.ChangeState(player.jumpState);
        }
        else if (player.Input.Dash.Down)
        {
            state.ChangeState(player.dashState);
        }
        else if (player.Input.MeleeAttack.Down)
        {
            state.ChangeState(player.meleeAttackState);
        }
        else if (!player.Input.Horizontal.Down)
        {
            state.ChangeState(player.idleState);
        }
    }
}
