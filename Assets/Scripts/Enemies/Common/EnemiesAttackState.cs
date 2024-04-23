using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackState : EnemiesBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Attack);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        //Debug.Log("Enemies Base Attack");
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