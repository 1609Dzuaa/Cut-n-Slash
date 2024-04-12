using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    float _entryTime;

    public float EntryTime { get => _entryTime; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Roll);
        //Ở state này điều chỉnh Box Col sao cho nó né đc tên của Archer
        //Và cho phép Player Roll xuyên qua Enemies (DeadCells's mechanic)
        //x: 0.3274712; y: -0.7
        //Sử dụng properties bên Animation
        _entryTime = Time.time;
        Debug.Log("Roll");
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
        if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.RollSpeed, 0f);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.RollSpeed, 0f);
    }
}
