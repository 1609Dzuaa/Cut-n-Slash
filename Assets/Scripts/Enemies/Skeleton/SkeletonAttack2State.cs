using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack2State : EnemiesAttackState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.ESkeletonState.Attack2);
        Debug.Log("Skl Atk2");
    }

    public override void ExitState()
    {
        base.ExitState();
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
