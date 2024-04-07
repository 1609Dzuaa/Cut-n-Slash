using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerBaseState
{
    private float _entryTime = 0;

    public float EntryTime { get => _entryTime; set => _entryTime = value; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack2);
        _playerSM.StopAllCoroutines();
        _playerSM.Attack1State.EntryTime = 0;
        _entryTime = Time.time;
        _playerSM.StartCoroutine(_playerSM.BackToIdle());
        Debug.Log("Atk2");
    }

    public override void ExitState() { }

    public override void UpdateState()
    {
        /*if (Time.time - _entryTime >= _playerSM.DelayUpdateAttack)
            if (CheckIfCanAttack3())
                _playerSM.ChangeState(_playerSM.Attack3State);*/
    }

    private bool CheckIfCanAttack3()
    {
        return Input.GetMouseButtonDown(0) && _playerSM.IsOnGround;
    }

    public override void FixedUpdate()
    {
        if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.MoveSpeedWhileAttack, 0f);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.MoveSpeedWhileAttack, 0f);
    }

}