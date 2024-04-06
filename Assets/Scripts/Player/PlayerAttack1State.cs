using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1State : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack1);
        _playerSM.StartCoroutine(_playerSM.BackToIdle());
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
    }

    private bool CheckIfCanAttack2()
    {
        return /*Input.GetMouseButtonDown(0)*/ Input.GetKeyDown(KeyCode.E) && _playerSM.IsOnGround;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}