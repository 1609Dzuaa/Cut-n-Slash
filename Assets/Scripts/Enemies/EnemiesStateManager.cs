using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesStateManager : CharactersStateManager
{
    //Gom đống này vứt vào SO
    protected Transform _playerCheck;
    protected float _pCheckDistance;
    protected LayerMask _playerLayer;
    protected Transform _groundCheck;
    protected Transform _wallCheck;
    protected LayerMask _gwLayer;
    protected bool _hasDetectedPlayer;
    protected bool _hasDetectedGround;
    protected bool _hasDetectedWall;
    protected float _attackDelay;
    protected float _patrolTime;
    protected float _restTime;

    public float PCheckDistance { get => _pCheckDistance; }

    public bool HasDetectedPlayer { get => _hasDetectedPlayer; }

    public float AttackDelay { get => _attackDelay; }

    public float PatrolTime { get => _patrolTime; }

    public float RestTime { get => _restTime; }


    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }

    protected virtual void DetectPlayer()
    {
        //Tạo 1 obj vô hình cùng layer với Player block giữa các wall
        //Phân biệt nó vs Player = Tag

        /*if (BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Invisible).IsAllowToUpdate)
        {
            _hasDetectedPlayer = false;
            return;
        }*/

        if (!_isFacingRight)
        {

            /*RaycastHit2D hit = Physics2D.Raycast(_playerCheck.position, Vector2.left, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);

            if (hit && hit.collider.CompareTag(GameConstants.PLAYER_TAG))
                _hasDetectedPlayer = true;
            else
                _hasDetectedPlayer = false;
            _hasDetectedPlayer = Physics2D.Raycast(_playerCheck.position, Vector2.left, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);*/
        }
        else
        {
            /*RaycastHit2D hit = Physics2D.Raycast(_playerCheck.position, Vector2.right, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);
            if (hit && hit.collider.CompareTag(GameConstants.PLAYER_TAG))
                _hasDetectedPlayer = true;
            else
                _hasDetectedPlayer = false;

            _hasDetectedPlayer = Physics2D.Raycast(_playerCheck.position, Vector2.right, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);*/
        }
    }

}
