using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Land);
        Debug.Log("Land");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        //if (CheckIfCanIdle())
            //_playerSM.ChangeState(_playerSM.IdleState);
    }

    private bool CheckIfCanIdle()
    {
        //Nếu vận tốc 2 trục rất nhỏ VÀ đang trên nền thì coi như đang Idle
        return Mathf.Abs(_playerSM.GetRigidbody2D.velocity.x) < GameConstants.NEAR_ZERO_THRESHOLD
            && Mathf.Abs(_playerSM.GetRigidbody2D.velocity.y) < GameConstants.NEAR_ZERO_THRESHOLD
            && _playerSM.IsOnGround;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}