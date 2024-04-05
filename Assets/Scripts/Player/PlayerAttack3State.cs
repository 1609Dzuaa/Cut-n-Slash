using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack3);
        Debug.Log("Atk3");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanIdle())
            _playerSM.ChangeState(_playerSM.IdleState);
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