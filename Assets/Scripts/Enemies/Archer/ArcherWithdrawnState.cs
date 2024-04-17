using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWithdrawnState : EnemiesBaseState
{
    //Có bug từ withdrawn sang attack khiến withdrawn bị khựng lại giữa chừng
    //Lỗi do delay trigger attack (khả năng cao)

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
            _archerSM.GetRigidbody2D.AddForce(_archerSM.ArcherSO.WithdrawnForce * new Vector2(-1f, 1f), ForceMode2D.Impulse);
        else
            _archerSM.GetRigidbody2D.AddForce(_archerSM.ArcherSO.WithdrawnForce, ForceMode2D.Impulse);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
