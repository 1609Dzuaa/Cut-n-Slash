using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPatrolState : EnemiesPatrolState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _archerSM = (ArcherStateManager)_enemiesSM;
        Debug.Log("Archer Pt");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (_archerSM.WithdrawnCheck() && !_hasStartedFlip)
            _archerSM.ChangeState(_archerSM.GetArcherWithdrawnState());
        else
            base.UpdateState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
