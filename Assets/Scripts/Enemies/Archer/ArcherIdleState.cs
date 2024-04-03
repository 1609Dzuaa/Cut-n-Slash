using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : EnemiesIdleState
{
    //EnemiesStateManager _enemiesSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM = (EnemiesStateManager)_charactersSM;
        _charactersSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Idle);
        Debug.Log("Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        //if (CheckIfCanAttack())
            //_enemiesSM.ChangeState(_enemiesSM.GetAttackState());
    }

    /*protected virtual bool CheckIfCanAttack()
    {
        return _enemiesSM.HasDetectedPlayer;
    }*/

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}