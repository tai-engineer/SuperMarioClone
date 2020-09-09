using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackState : PlayerState
{
    public override void EnterState(PlayerStateController state, PlayerController player)
    {
        Debug.Log("State: Melee Attack");
        player.Combat.StartMeleeAttack();
        player.SetParameter(player.boolAttackParameter, true);
    }

    public override void ExitState(PlayerStateController state, PlayerController player)
    {
        player.SetParameter(player.boolAttackParameter, false);
    }

    public override void FixedUpdate(PlayerStateController state, PlayerController player)
    {
        player.CheckGround();
        player.CheckCeiling();

        if (player.IsGrounded)
        {
            player.GroundVerticalMovement();
            player.GroundHorizontalMovement();
        }
        else
        {
            player.AirborneVerticalMovement();
            player.AirborneHorizontalMovement();
        }

        player.Combat.CheckMeleeAttackHitBox();
    }

    public override void Update(PlayerStateController state, PlayerController player)
    {
        if(!player.IsAttacking && player.IsGrounded)
        {
            if(player.Input.Jump.Down)
            {
                state.ChangeState(player.jumpState);
            }
            else if(player.Input.Horizontal.Down)
            {
                state.ChangeState(player.runState);
            }
            else
            {
                state.ChangeState(player.idleState);
            }
        }
        else if(!player.IsAttacking)
        {
            state.ChangeState(player.idleState);
        }
    }
}
