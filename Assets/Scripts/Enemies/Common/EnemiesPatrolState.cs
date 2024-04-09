﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatrolState : EnemiesBaseState
{
    protected float _entryTime;
    protected bool _hasTriggeredAttack;
    protected bool _hasStartedFlip;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Patrol);
        _entryTime = Time.time;
        Debug.Log("Enemies Base Patrol");
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
        else if (CheckIfCanRest())
            _enemiesSM.ChangeState(_enemiesSM.GetIdleState());
        else if (_enemiesSM.HasDetectedPlayerBackward)
        {
            //Player ở phía sau thì Idle 1 lúc r flip
            _enemiesSM.ChangeState(_enemiesSM.GetIdleState());
            _enemiesSM.GetIdleState().HasStartedFlip = true;
            _enemiesSM.StartCoroutine(_enemiesSM.DelayFlip());
            Debug.Log("Player Backward");
        }
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