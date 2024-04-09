using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : EnemiesAttackState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        Debug.Log("Archer Atk");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
