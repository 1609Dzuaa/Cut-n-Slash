using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1State : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack1);
        Debug.Log("Atk1");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanAttack2())
            _playerSM.ChangeState(_playerSM.Attack2State);
        else if (CheckIfCanIdle())
            _playerSM.ChangeState(_playerSM.IdleState);
    }

    private bool CheckIfCanAttack2()
    {
        return Input.GetMouseButtonDown(0) && _playerSM.IsOnGround;
    }

    private bool CheckIfCanIdle()
    {
        return _playerSM.DirX == 0 && _playerSM.IsOnGround && !Input.GetMouseButtonDown(0);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}