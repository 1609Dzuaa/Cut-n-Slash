using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack3);
        _playerSM.Attack2State.EntryTime = 0;
        Debug.Log("Atk3");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState() { }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}