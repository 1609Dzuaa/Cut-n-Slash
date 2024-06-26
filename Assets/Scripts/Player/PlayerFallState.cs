﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    float _entryTime;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Fall);
        _playerSM.GetAnim.SetBool(GameConstants.ANIM_PARA_ON_GROUND, _playerSM.IsOnGround);
        _playerSM.GetRigidbody2D.gravityScale = _playerSM.GravScale;
        _entryTime = Time.time;
        //Debug.Log("Fall " + _playerSM.IsOnGround);
    }

    public override void ExitState()
    {
        _playerSM.GetRigidbody2D.gravityScale = GameConstants.PLAYER_DEFAULT_GRAV;
        //_playerSM.GetAnim.SetBool(GameConstants.ANIM_PARA_ON_GROUND, _playerSM.IsOnGround);
    }

    public override void UpdateState()
    {
        if (CheckIfCanLand())
            _playerSM.ChangeState(_playerSM.LandingState);
        else if (CheckIfCanIdle())
            _playerSM.ChangeState(_playerSM.IdleState);
        else if (CheckIfCanDash())
            _playerSM.ChangeState(_playerSM.DashState);
    }

    private bool CheckIfCanIdle()
    {
        //Nếu vận tốc 2 trục rất nhỏ VÀ đang trên nền thì coi như đang Idle
        return Mathf.Abs(_playerSM.GetRigidbody2D.velocity.x) < GameConstants.NEAR_ZERO_THRESHOLD
            && Mathf.Abs(_playerSM.GetRigidbody2D.velocity.y) < GameConstants.NEAR_ZERO_THRESHOLD
            && _playerSM.IsOnGround;
    }

    private bool CheckIfCanLand()
    {
        //Sẽ chuyển sang state Land nếu fall quá thời gian TimeCanLanding
        return Mathf.Abs(_playerSM.GetRigidbody2D.velocity.x) < GameConstants.NEAR_ZERO_THRESHOLD
            && Mathf.Abs(_playerSM.GetRigidbody2D.velocity.y) < GameConstants.NEAR_ZERO_THRESHOLD
            && Time.time - _entryTime >= _playerSM.TimeCanLanding
            && _playerSM.IsOnGround;
    }

    private bool CheckIfCanDash()
    {
        float dashEntryTime = _playerSM.DashState.EntryTime;
        return Input.GetKeyDown(KeyCode.E) && Time.time - dashEntryTime >= _playerSM.DashDelay;
    }

    public override void FixedUpdate()
    {
        /*if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.WalkSpeed * _playerSM.DirX, _playerSM.GetRigidbody2D.velocity.y);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.WalkSpeed * _playerSM.DirX, _playerSM.GetRigidbody2D.velocity.y);
        //if (_playerSM.DirX != 0)*/
        _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.WalkSpeed * _playerSM.DirX, _playerSM.GetRigidbody2D.velocity.y);
    }
}
