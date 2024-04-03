﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesStateManager : CharactersStateManager
{
    [Header("Check Properties")]
    [SerializeField] protected Transform _playerCheck;
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected Transform _wallCheck;

    protected RaycastHit2D _pRayHit;
    protected bool _hasDetectedPlayer;
    protected bool _hasDetectedGround;
    protected bool _hasDetectedWall;

    [Header("SO Data")]
    [SerializeField] EnemiesSO _enemiesSO;

    public bool HasDetectedPlayer { get => _hasDetectedPlayer; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetupProperties()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.y) >= 180f)
            _isFacingRight = false;
        //Debug.Log("IfR, yAngles: " + _isFacingRight + ", " + transform.rotation.eulerAngles.y);
    }

    protected override void Update()
    {
        base.Update();
        DetectPlayer();
        DrawRayDetectPlayer();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }

    protected virtual void DetectPlayer()
    {
        /*if (BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Invisible).IsAllowToUpdate)
        {
            _hasDetectedPlayer = false;
            return;
        }*/

        //Tạo 1 obj vô hình cùng layer với Player block giữa các wall
        //Phân biệt nó vs Player = Tag
        if (!_isFacingRight)
        {
            _pRayHit = Physics2D.Raycast(_playerCheck.position, Vector2.left, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);

            if (_pRayHit && _pRayHit.collider.CompareTag(GameConstants.PLAYER_TAG))
                _hasDetectedPlayer = true;
            else
                _hasDetectedPlayer = false;
        }
        else
        {
            _pRayHit = Physics2D.Raycast(_playerCheck.position, Vector2.right, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);

            if (_pRayHit && _pRayHit.collider.CompareTag(GameConstants.PLAYER_TAG))
                _hasDetectedPlayer = true;
            else
                _hasDetectedPlayer = false;
        }
    }

    protected virtual void DrawRayDetectPlayer()
    {
        if (_hasDetectedPlayer)
        {
            if (!_isFacingRight)
                Debug.DrawRay(_playerCheck.position, Vector2.left * _enemiesSO.PlayerCheckDistance, Color.red);
            else
                Debug.DrawRay(_playerCheck.position, Vector2.right * _enemiesSO.PlayerCheckDistance, Color.red);
        }
        else
        {
            if (!_isFacingRight)
                Debug.DrawRay(_playerCheck.position, Vector2.left * _enemiesSO.PlayerCheckDistance, Color.green);
            else
                Debug.DrawRay(_playerCheck.position, Vector2.right * _enemiesSO.PlayerCheckDistance, Color.green);
        }
    }

    protected virtual void SelfDestroy() { Destroy(gameObject); }

}