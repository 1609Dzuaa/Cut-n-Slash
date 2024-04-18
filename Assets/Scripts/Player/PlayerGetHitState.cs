using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitState : PlayerBaseState
{
    float _entryTime;

    public float EntryTime { get => _entryTime; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.GetHit);
        _entryTime = Time.time;
        _playerSM.IsApplyGetHitEffect = true;
        Debug.Log("P Get Hit");
    }

    public override void ExitState()
    {
        _playerSM.HasGetHit = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
