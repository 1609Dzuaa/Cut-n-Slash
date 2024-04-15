using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : EnemiesIdleState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _archerSM = (ArcherStateManager)_enemiesSM;
        //Debug.Log("Archer Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        //Tận dụng Update từ base class
        float withdrawnEntryTime = _archerSM.GetArcherWithdrawnState().EntryTime;
        float withdrawnDelay = _archerSM.WithdrawnDelay;

        if (_archerSM.WithdrawnCheck() && !_hasStartedFlip
            && Time.time - withdrawnEntryTime >= withdrawnDelay)
            _archerSM.ChangeState(_archerSM.GetArcherWithdrawnState());
        else if (_archerSM.TeleportCheck())
            _archerSM.ChangeState(_archerSM.GetArcherTeleportState());
        else
            base.UpdateState();
    }

    public override void FixedUpdate() { }
}