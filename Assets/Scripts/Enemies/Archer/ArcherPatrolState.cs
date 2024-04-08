using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPatrolState : EnemiesPatrolState
{
    //protected EnemiesStateManager _enemiesSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Patrol);
        Debug.Log("Pt");
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
