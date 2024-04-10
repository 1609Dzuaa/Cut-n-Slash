using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float _entryTime;

    public float EntryTime { get => _entryTime; }

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _playerSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EPlayerState.Dash);
        _entryTime = Time.time;
        HandleDash();
        //Debug.Log("Dash");
    }

    public override void ExitState() { }

    public override void UpdateState()
    {
        if (Time.time - _entryTime >= _playerSM.DelayUpdateDash)
        {
            //Trả grav về lại như cũ sau khi cho phép Update
            _playerSM.GetRigidbody2D.gravityScale = GameConstants.PLAYER_DEFAULT_GRAV;

            if (CheckIfCanIdle())
                _playerSM.ChangeState(_playerSM.IdleState);
            /*else if (CheckIfCanRun())
                _playerStateManager.ChangeState(_playerStateManager.runState);
            else if (CheckIfCanFall())
            {
                //Đảm bảo 0 dashing quá xa sau khi fall
                _playerStateManager.GetRigidBody2D().velocity = Vector2.zero;

                _playerStateManager.ChangeState(_playerStateManager.fallState);
            }
            else if (CheckIfCanWallSlide())
                _playerStateManager.ChangeState(_playerStateManager.wallSlideState);*/
        }
    }

    private bool CheckIfCanIdle()
    {
        return Mathf.Abs(_playerSM.DirX) < GameConstants.NEAR_ZERO_THRESHOLD && _playerSM.IsOnGround;
    }

    private void HandleDash()
    {
        //Vô hiệu hoá grav khi dash (cho ảo hơn)
        _playerSM.GetRigidbody2D.gravityScale = 0f;
        //SoundsManager.Instance.PlaySfx(GameEnums.ESoundName.PlayerDashSfx, 0.5f);
        //_playerSM.GetTrailRenderer().emitting = true;
        //_playerSM.gameObject.layer = LayerMask.NameToLayer(GameConstants.IGNORE_ENEMIES_LAYER);
        //BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Shield).gameObject.layer = LayerMask.NameToLayer(GameConstants.IGNORE_ENEMIES_LAYER);

        //Set thẳng thằng velo luôn cho khỏi bị override
        //Vì nếu set theo addforce thì lúc fall nó sẽ dash dần xuống 1 đoạn
        //Chứ 0 phải dash thẳng trên 0 1 đoạn

        if (_playerSM.IsFacingRight)
            _playerSM.GetRigidbody2D.velocity = new Vector2(_playerSM.DashSpeed, 0f);
        else
            _playerSM.GetRigidbody2D.velocity = new Vector2(-_playerSM.DashSpeed, 0f);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
