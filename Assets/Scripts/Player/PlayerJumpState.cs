using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Jump);

        //Jump
        float xVelo = _playerSM.GetRigidbody2D.velocity.x;
        float yVelo = _playerSM.JumpForce;
        _playerSM.GetRigidbody2D.velocity = new(xVelo, yVelo);

        Debug.Log("Jump");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanFall())
            _playerSM.ChangeState(_playerSM.FallState);
    }

    private bool CheckIfCanFall()
    {
        return _playerSM.GetRigidbody2D.velocity.y < -GameConstants.NEAR_ZERO_THRESHOLD;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}