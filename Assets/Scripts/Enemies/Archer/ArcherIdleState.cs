using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : EnemiesIdleState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _archerSM = (ArcherStateManager)charactersSM;
        Debug.Log("Archer Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        //base.UpdateState();
        if (!_hasStartedFlip)
        {
            if (_archerSM.WithdrawnCheck())
                _archerSM.ChangeState(_archerSM.GetArcherWithdrawnState());
            else if (CheckIfCanAttack())
            {
                _hasTriggeredAttack = true;
                _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
                Debug.Log("Can attack");
            }
            else if (CheckIfCanPatrol())
                _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());
            else if (_enemiesSM.HasDetectedPlayerBackward)
            {
                _hasStartedFlip = true;
                _enemiesSM.StartCoroutine(_enemiesSM.DelayFlip());
                Debug.Log("Player Backward");
            }
        }
        /*if (_archerSM.WithdrawnCheck())
            _archerSM.ChangeState(_archerSM.GetArcherWithdrawnState());
        else if (CheckIfCanAttack())
        {
            _hasTriggeredAttack = true;
            _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
        }
        else if (CheckIfCanPatrol())
            _enemiesSM.ChangeState(_enemiesSM.GetPatrolState());*/
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}