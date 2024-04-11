using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateManager : EnemiesStateManager
{
    SkeletonAttack2State _attack2State = new();

    //Event đặt cuối frame animation Attack 1
    private void ChangeToAttack2()
    {
        ChangeState(_attack2State);    
    }

    private void WakeUpRigidBody2D()
    {
        _rb.WakeUp();
    }
}
