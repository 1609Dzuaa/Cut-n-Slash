using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM = (EnemiesStateManager)_charactersSM;
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Attack);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        Debug.Log("Attack");
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