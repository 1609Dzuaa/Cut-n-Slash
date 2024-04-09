using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWithdrawnState : EnemiesBaseState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        _archerSM = (ArcherStateManager)charactersSM;
        _archerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EArcherState.Withdrawn);
        HandleWithdrawn();
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
