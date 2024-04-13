using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEyeStateManager : EnemiesStateManager
{
    DevilEyeAttack2State _attack2State = new();

    private void ChangeToAttack2()
    {
        ChangeState(_attack2State);
    }
}
