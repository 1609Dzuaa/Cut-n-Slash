using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesIdleState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;
    protected float _entryTime;
    protected bool _hasTriggeredAttack;

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
        _hasTriggeredAttack = false;
    }

    public override void UpdateState()
    {
        if (CheckIfCanAttack())
        {
            _hasTriggeredAttack = true;
            _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
        }
        else if (CheckIfCanPatrol())
            _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());
    }

    protected virtual bool CheckIfCanPatrol()
    {
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().RestTime;
    }

    protected virtual bool CheckIfCanAttack()
    {
        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack;   
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
