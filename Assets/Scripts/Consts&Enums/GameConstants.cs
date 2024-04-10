using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    #region Tags

    public const string PLAYER_TAG = "Player";
    public const string PLAYER_SWORD_TAG = "PlayerSword";
    public const string TRAP_TAG = "Trap";
    public const string ENEMIES_WEAPON_TAG = "EnemiesWeapon";

    #endregion

    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
    public const string JUMP_BUTTON = "Jump";

    public const float NEAR_ZERO_THRESHOLD = 0.1f;
    public const float PLAYER_DEFAULT_GRAV = 1.7f;

    #region Animator's Parameters

    public const string STATE_ANIM = "state";
    public const string ANIM_PARA_ON_GROUND = "isOnGround";

    #endregion
}
