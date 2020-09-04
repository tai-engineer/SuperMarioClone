using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioRunState : MarioState
{
    public override void EnterState(MarioStateController state, MarioController player)
    {
        Debug.Log("State: Run");
        player.SetParameter(player.boolRunParameter, true);
    }

    public override void ExitState(MarioStateController state, MarioController player)
    {
        player.SetParameter(player.boolRunParameter, false);
    }

    public override void FixedUpdate(MarioStateController state, MarioController player)
    {
        player.GroundVericalMovement();
        player.GroundHorizontalMovement();
    }

    public override void Update(MarioStateController state, MarioController player)
    {
        if (player.Input.Jump.Down)
        {
            state.ChangeState(player.jumpState);
        }
        else if (!player.Input.Horizontal.Down)
        {
            state.ChangeState(player.idleState);
        }

        if(player.IsBigTransform)
        {
            state.ChangeState(player.idleState);
        }

    }
}
