using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGetHitState : EnemiesBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.GetHit);
        Debug.Log("Enemies Base GH");
    }

    public override void ExitState()
    {
        _enemiesSM.HasGetHit = false;
    }

    public override void UpdateState()
    {
        Debug.Log("GetHit Update");
    }

    public override void FixedUpdate() { }
}