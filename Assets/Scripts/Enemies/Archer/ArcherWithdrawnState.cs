using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWithdrawnState : EnemiesBaseState
{
    ArcherStateManager _archerSM;
    float _entryTime;

    public float EntryTime { get => _entryTime; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        _archerSM = (ArcherStateManager)charactersSM;
        _archerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EArcherState.Withdrawn);
        HandleWithdrawn();
        _entryTime = Time.time;
        Debug.Log("Withdrawn");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleWithdrawn()
    {
        if (_archerSM.IsFacingRight)
            _archerSM.GetRigidbody2D.velocity = _archerSM.WithdrawnForce * new Vector2(-1f, 1f);
        else
            _archerSM.GetRigidbody2D.velocity = _archerSM.WithdrawnForce;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
