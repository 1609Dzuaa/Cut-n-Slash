using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : EnemiesAttackState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EArcherState.Attack);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        Debug.Log("Archer Atk");

        /*
         _enemiesSM = (EnemiesStateManager)_charactersSM;
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Attack);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;*/
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        //if (CheckIfCanAttack())
        //_archerSM.ChangeState(_archerSM.GetAttackState());
    }

    /*protected virtual bool CheckIfCanAttack()
    {
        return _archerSM.HasDetectedPlayer;
    }*/

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
