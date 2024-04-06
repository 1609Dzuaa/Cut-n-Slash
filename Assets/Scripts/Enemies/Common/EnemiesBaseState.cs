using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBaseState : CharacterBaseState
{
    protected EnemiesStateManager _enemiesSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        _enemiesSM = (EnemiesStateManager)charactersSM;
        //Debug.Log("init eSM");
    }
}
