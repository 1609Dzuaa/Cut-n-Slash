using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    #region Tags

    public const string UNTAGGED_TAG = "Untagged";
    public const string PLAYER_TAG = "Player";
    public const string PLAYER_SWORD_TAG = "PlayerSword";
    public const string TRAP_TAG = "Trap";
    public const string ENEMIES_TAG = "Enemies";
    public const string ENEMIES_WEAPON_TAG = "EnemiesWeapon";
    public const string GOLD_COIN_TAG = "GoldCoin";

    #endregion

    #region Layers

    public const string IGNORE_ENEMIES_LAYER = "Ignore Enemies";

    #endregion

    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
    public const string JUMP_BUTTON = "Jump";

    public const float NEAR_ZERO_THRESHOLD = 0.1f;
    public const float PLAYER_DEFAULT_GRAV = 1.7f;
    public const float FLIPABLE_OFFSET = 1.5f;

    #region Animator's Parameters

    public const string STATE_ANIM = "state";
    public const string ANIM_PARA_ON_GROUND = "isOnGround";
    public const string TRAP_ANIM_PARA = "On";

    #endregion
}
