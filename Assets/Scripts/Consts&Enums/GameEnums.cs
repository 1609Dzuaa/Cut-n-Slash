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
        GetHit = 11,
        Die = 12,
    }

    //Dùng chung
    public enum EEnemiesCommonState
    {
        Idle = 0,
        Patrol = 1,
        Attack = 2,
        GetHit = 3,
        Die = 4
    }

    public enum EArcherState
    {
        Idle = 0,
        Patrol = 1,
        Withdrawn = 2,
        Attack = 3,
        GetHit = 4,
        Die = 5
    }

    public enum EEvents
    {
        None = 0,
        EnemiesOnReceiveDamage = 1,
        PlayerOnReceiveDamage = 2,
    }
}
