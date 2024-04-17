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
        //_enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        _entryTime = Time.time;
        //Debug.Log("Enemies Base Idle");
    }

    public override void ExitState()
    {
        _hasTriggeredAttack = false;
    }

    public override void UpdateState()
    {
        if (!_hasStartedFlip)
        {
            if (CheckIfCanAttack())
            {
                _hasTriggeredAttack = true;
                _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
            }
            else if (CheckIfCanChase())
                _enemiesSM.ChangeState(_enemiesSM.GetChaseState());
            else if (CheckIfCanPatrol())
                _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());
            else if (_enemiesSM.HasDetectedPlayerBackward)
            {
                //Player ở phía sau thì Idle 1 lúc r flip
                _hasStartedFlip = true;
                _enemiesSM.StartCoroutine(_enemiesSM.DelayFlip());
                //Debug.Log("Player Backward");
            }
        }
    }

    protected virtual bool CheckIfCanChase()
    {
        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack;
    }

    protected virtual bool CheckIfCanPatrol()
    {
        return Time.time - _entryTime > _enemiesSM.GetEnemiesSO().RestTime && !_hasTriggeredAttack;
    }

    protected virtual bool CheckIfCanAttack()
    {
        Vector2 currentPos = _enemiesSM.transform.position;
        Vector2 playerPos = _enemiesSM.PlayerRef.position;
        float attackableDist = _enemiesSM.GetEnemiesSO().AttackableDistance;

        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack
            && Vector2.Distance(currentPos, playerPos) <= attackableDist;
    }

    public override void FixedUpdate() { }
}
