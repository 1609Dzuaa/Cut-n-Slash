using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        //Jump
        float xVelo = _playerSM.GetRigidbody2D.velocity.x;
        float yVelo = _playerSM.JumpForce;
        _playerSM.GetRigidbody2D.velocity = new(xVelo, yVelo);

        //Để false thẳng luôn vì nếu để _isOG thì lực Jump 0 đủ mạnh dẫn đến vẫn đang OG
        _playerSM.GetAnim.SetBool(GameConstants.ANIM_PARA_ON_GROUND, false);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Jump);

        //Debug.Log("Jump" + _playerSM.IsOnGround);
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