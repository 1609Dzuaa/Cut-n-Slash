using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Idle);
        _playerSM.GetAnim.SetBool(GameConstants.ANIM_PARA_ON_GROUND, true);
        _playerSM.GetRigidbody2D.velocity = Vector2.zero;
        Debug.Log("Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (CheckIfCanWalk())
            _playerSM.ChangeState(_playerSM.WalkState);
        else if (CheckIfCanJump())
            _playerSM.ChangeState(_playerSM.JumpState);
        else if (CheckIfCanDash())
            _playerSM.ChangeState(_playerSM.DashState);
        else if (CheckIfCanFall())
            _playerSM.ChangeState(_playerSM.FallState);
        else if (CheckIfCanAttack(_playerSM.Attack2State.EntryTime, false))
            _playerSM.ChangeState(_playerSM.Attack3State);
        else if (CheckIfCanAttack(_playerSM.Attack1State.EntryTime, false))
            _playerSM.ChangeState(_playerSM.Attack2State);
        else if (CheckIfCanAttack(0, true))
            _playerSM.ChangeState(_playerSM.Attack1State);
        else if (CheckIfCanRoll())
            _playerSM.ChangeState(_playerSM.RollState);
    }

    private bool CheckIfCanWalk()
    {
        return _playerSM.DirX != 0 && _playerSM.IsOnGround;
    }

    private bool CheckIfCanJump()
    {
        return Input.GetButtonDown(GameConstants.JUMP_BUTTON) && _playerSM.IsOnGround;
    }

    private bool CheckIfCanFall()
    {
        return _playerSM.DirY == 0 && !_playerSM.IsOnGround;// && !_playerStateManager.CanJump;
        //Idle => Fall có thể là đứng yên, bị 1 vật khác
        //tác dụng lực vào đẩy rơi xuống dưới
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

    private bool CheckIfCanDash()
    {
        float dashEntryTime = _playerSM.DashState.EntryTime;
        return Input.GetKeyDown(KeyCode.E) && Time.time - dashEntryTime >= _playerSM.DashDelay;
    }

    private bool CheckIfCanRoll()
    {
        return Input.GetKeyDown(KeyCode.LeftControl) && _playerSM.IsOnGround;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

/*
 : PlayerBaseState
{
    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
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
}*/