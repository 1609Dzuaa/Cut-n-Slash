using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesIdleState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;
    protected float _entryTime;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM = (EnemiesStateManager)_charactersSM;
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Idle);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        _entryTime = Time.time;
        Debug.Log("Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanAttack())
            _enemiesSM.ChangeState(_enemiesSM.GetAttackState());
        else if (CheckIfCanPatrol())
            _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());
    }

    protected virtual bool CheckIfCanPatrol()
    {
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().RestTime;
    }

    protected virtual bool CheckIfCanAttack()
    {
        return _enemiesSM.HasDetectedPlayer;   
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
