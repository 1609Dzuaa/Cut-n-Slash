using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesIdleState : EnemiesBaseState
{
    protected float _entryTime;
    protected bool _hasTriggeredAttack;
    protected bool _hasStartedFlip;

    public bool HasStartedFlip { get => _hasStartedFlip; set => _hasStartedFlip = value; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Idle);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        _entryTime = Time.time;
        Debug.Log("Enemies Base Idle");
    }

    public override void ExitState()
    {
        _hasTriggeredAttack = false;
    }

    public override void UpdateState()
    {
        if(!_hasStartedFlip)
        {
            if (CheckIfCanAttack())
            {
                _hasTriggeredAttack = true;
                _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
            }
            else if (CheckIfCanPatrol())
                _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());
            else if (_enemiesSM.HasDetectedPlayerBackward)
            {
                //Player ở phía sau thì Idle 1 lúc r flip
                _hasStartedFlip = true;
                _enemiesSM.StartCoroutine(_enemiesSM.DelayFlip());
                Debug.Log("Player Backward");
            }
        }
    }

    protected virtual bool CheckIfCanPatrol()
    {
        //Coi lại biến hasTriggeredAttack
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().RestTime && !_hasTriggeredAttack;
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
