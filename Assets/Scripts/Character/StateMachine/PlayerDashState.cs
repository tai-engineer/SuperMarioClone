using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public override void EnterState(PlayerStateController state, PlayerController player)
    {
        Debug.Log("State: Dash");
        player.StartDash();
        player.CreateDashEffect();
        player.SetParameter(player.boolDashParameter, true);
    }

    public override void ExitState(PlayerStateController state, PlayerController player)
    {
        player.FinishDash();
        player.SetParameter(player.boolDashParameter, false);
    }

    public override void FixedUpdate(PlayerStateController state, PlayerController player)
    {
        player.CheckGround();
        player.GroundVerticalMovement();
        player.GroundHorizontalMovement();
        player.HorizontalDashMovement();
    }

    public override void Update(PlayerStateController state, PlayerController player)
    {
        if (!player.IsDashing)
        {
            state.ChangeState(player.idleState);
        }
        else if (player.Input.Jump.Down)
        {
            state.ChangeState(player.jumpState);
        }
        else if (player.Input.MeleeAttack.Down)
        {
            state.ChangeState(player.meleeAttackState);
        }
    }
}
