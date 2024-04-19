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

    public override void UpdateState() { }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}