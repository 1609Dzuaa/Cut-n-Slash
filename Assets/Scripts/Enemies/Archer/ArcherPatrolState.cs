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
        float withdrawnEntryTime = _archerSM.GetArcherWithdrawnState().EntryTime;
        float withdrawnDelay = _archerSM.ArcherSO.WithdrawnDelay;

        if (_archerSM.WithdrawnCheck() && !_hasStartedFlip
            && Time.time - withdrawnEntryTime >= withdrawnDelay)
        {
            if (_archerSM.TeleportCheck())
                _archerSM.ChangeState(_archerSM.GetArcherTeleportState());
            else
                _archerSM.ChangeState(_archerSM.GetArcherWithdrawnState());
        }
        else
            base.UpdateState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
