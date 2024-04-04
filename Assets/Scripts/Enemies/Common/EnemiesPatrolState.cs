using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatrolState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;
    protected float _entryTime;
    protected bool _hasTriggeredAttack;

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
        if (CheckIfCanAttack())
        {
            _hasTriggeredAttack = true;
            _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
        }
        else if (CheckIfCanRest())
            _enemiesSM.ChangeState(_enemiesSM.GetIdleState());
    }

    protected virtual bool CheckIfCanRest()
    {
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().PatrolTime;
    }

    protected virtual bool CheckIfCanAttack()
    {
        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack;
    }

    public override void FixedUpdate()
    {
        if (_enemiesSM.IsFacingRight)
            _enemiesSM.GetRigidbody2D.velocity = new Vector2(_enemiesSM.GetEnemiesSO().PatrolSpeed, _enemiesSM.GetRigidbody2D.velocity.y);
        else
            _enemiesSM.GetRigidbody2D.velocity = new Vector2(-_enemiesSM.GetEnemiesSO().PatrolSpeed, _enemiesSM.GetRigidbody2D.velocity.y);
    }
}