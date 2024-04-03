using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatrolState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;
    protected float _entryTime;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM = (EnemiesStateManager)_charactersSM;
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Patrol);
        _entryTime = Time.time;
        Debug.Log("Patrol");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanRest())
            _enemiesSM.ChangeState(_enemiesSM.GetIdleState());
        else if (CheckIfCanAttack())
            _enemiesSM.ChangeState(_enemiesSM.GetAttackState());
    }

    protected virtual bool CheckIfCanRest()
    {
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().PatrolTime;
    }

    protected virtual bool CheckIfCanAttack()
    {
        return _enemiesSM.HasDetectedPlayer;
    }

    public override void FixedUpdate()
    {
        if (_enemiesSM.IsFacingRight)
            _enemiesSM.GetRigidbody2D.velocity = new Vector2(_enemiesSM.GetEnemiesSO().PatrolSpeed, 0f);
        else
            _enemiesSM.GetRigidbody2D.velocity = new Vector2(-_enemiesSM.GetEnemiesSO().PatrolSpeed, 0f);
    }
}