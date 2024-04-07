using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnums
{
    public enum EPlayerState
    { 
        Idle = 0, 
        Walk = 1, 
        Run = 2, 
        Jump = 3, 
        Fall = 4,
        Attack1 = 5,
        Attack2 = 6,
        Attack3 = 7,
        Dash = 8,
        Roll = 9,
        Land = 10,
    }

    //Dùng chung
    public enum EEnemiesCommonState
    { Idle, Patrol, Attack, GetHit, Die }

    public enum EArcherState
    { Idle, Patrol, Withdrawn, Attack, GetHit, Die }

    public enum EEvents
    {
        None = 0,
        EnemiesOnReceiveDamage = 1,
        PlayerOnReceiveDamage = 2,
    }
}
