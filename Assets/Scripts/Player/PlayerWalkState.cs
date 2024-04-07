﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Walk);
        Debug.Log("Walk");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanIdle())
            _playerSM.ChangeState(_playerSM.IdleState);
        else if (CheckIfCanAttack(_playerSM.Attack2State.EntryTime, false))
            _playerSM.ChangeState(_playerSM.Attack3State);
        else if (CheckIfCanAttack(_playerSM.Attack1State.EntryTime, false))
            _playerSM.ChangeState(_playerSM.Attack2State);
        else if (CheckIfCanAttack(0, true))
            _playerSM.ChangeState(_playerSM.Attack1State);
        else if (CheckIfCanJump())
            _playerSM.ChangeState(_playerSM.JumpState);
        else if (CheckIfCanFall())
            _playerSM.ChangeState(_playerSM.FallState);
    }

    private bool CheckIfCanIdle()
    {
        return _playerSM.DirX == 0 && _playerSM.IsOnGround;
    }

    private bool CheckIfCanJump()
    {
        return Input.GetButtonDown(GameConstants.JUMP_BUTTON) && _playerSM.IsOnGround;
    }

    private bool CheckIfCanFall()
    {
        return !_playerSM.IsOnGround && !Input.GetButtonDown(GameConstants.JUMP_BUTTON);
    }

    private bool CheckIfCanAttack(float AtkEntryTime, bool isFirstAtk)
    {
        //First Atk bị block ở đây
        if (isFirstAtk)
            return Input.GetMouseButtonDown(0) && _playerSM.IsOnGround;

        float enableTime = _playerSM.EnableComboTime;

        return Input.GetMouseButtonDown(0) && AtkEntryTime != 0
            && Time.time - AtkEntryTime <= enableTime
            && _playerSM.IsOnGround;
    }

    public override void FixedUpdate()
    {
        if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.WalkSpeed, _playerSM.GetRigidbody2D.velocity.y);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.WalkSpeed, _playerSM.GetRigidbody2D.velocity.y);
    }
}