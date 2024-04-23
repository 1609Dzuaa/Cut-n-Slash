using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1State : PlayerBaseState
{
    private float _entryTime = 0;

    public float EntryTime { get => _entryTime; set => _entryTime = value; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Attack1);
        _playerSM.StartCoroutine(_playerSM.BackToIdle());
        _entryTime = Time.time;
        //Debug.Log("Atk1");
    }

    public override void ExitState() { }

    public override void UpdateState() { }

    public override void FixedUpdate()
    {
        if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.MoveSpeedWhileAttack, 0f);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.MoveSpeedWhileAttack, 0f);
    }

}