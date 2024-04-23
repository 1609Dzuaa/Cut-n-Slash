using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnums;

public struct DamageReceiveInfor
{
    public string ID;
    public float DamageReceive;

    public DamageReceiveInfor(string id, float damage)
    {
        ID = id;
        DamageReceive = damage;
    }
}

public class EnemiesStateManager : CharactersStateManager
{
    [Header("Check Properties")]
    [SerializeField] protected Transform _playerCheck;
    [SerializeField, Tooltip("Check Player ở phía sau")] protected Transform _playerCheck2;
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected Transform _wallCheck;

    [Header("SO Data")]
    [SerializeField] protected EnemiesSO _enemiesSO;

    [Header("Player Reference")]
    [SerializeField] protected Transform _playerRef;

    [Header("HP Ref")]
    [SerializeField] protected Image _hpFill;
    [SerializeField] protected GameObject _hpBar;

    #region States

    protected EnemiesIdleState _idleState = new();
    protected EnemiesPatrolState _patrolState = new();
    protected EnemiesAttackState _attackState = new();
    protected EnemiesGetHitState _getHitState = new();
    protected EnemiesDieState _dieState = new();
    protected EnemiesChaseState _chaseState = new();
    protected EnemiesAttack2State _attack2State = new();

    #endregion

    protected RaycastHit2D _detectPlayerRayFront;
    protected RaycastHit2D _detectPlayerRayBack;
    protected bool _hasDetectedPlayer;
    protected bool _hasDetectedPlayerBackward;
    protected bool _hasDetectedGround;
    protected bool _hasDetectedWall;
    protected bool _hasGetHit;
    protected float _currentHP;
    protected string _enemyID;

    public EnemiesSO GetEnemiesSO() { return _enemiesSO; }

    public EnemiesIdleState GetIdleState() { return _idleState; }

    public EnemiesPatrolState GetPatrolState() { return _patrolState; }

    public EnemiesDieState GetDieState() { return _dieState; }

    public EnemiesChaseState GetChaseState() { return _chaseState; }

    public bool HasDetectedPlayer { get => _hasDetectedPlayer; }

    public bool HasDetectedPlayerBackward { get => _hasDetectedPlayerBackward; }

    public bool HasGetHit { get => _hasGetHit; set => _hasGetHit = value; }

    public Transform PlayerRef { get => _playerRef; }

    public string ID { get => _enemyID; }

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
        base.SetupProperties();
        _state = _idleState;
        _state.EnterState(this);
        _currentHP = _enemiesSO.BaseHP;
        _enemyID = Guid.NewGuid().ToString();
        EventsManager.Instance.SubcribeToAnEvent(EEvents.EnemiesOnReceiveDamage, ReceiveDamage);
        //Debug.Log("IfR, yAngles: " + _isFacingRight + ", " + transform.rotation.eulerAngles.y);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.EnemiesOnReceiveDamage, ReceiveDamage);
    }

    protected override void Update()
    {
        base.Update();
        if (_state is EnemiesDieState) return;
        DrawRayDetectPlayer();
        DrawRayDetectWall();
        DrawRayDetectGround();
        HandleChangeDirection();
        Debug.Log("HP: " + _currentHP);
        //Debug.Log("Front, Back: " + _hasDetectedPlayer + ", " + _hasDetectedPlayerBackward);
    }

    protected override void FixedUpdate()
    {
        if (_state is EnemiesDieState) return;
        base.FixedUpdate();
        DetectPlayer();
        DetectWall();
        DetectGround();
    }

    public override void ChangeState(CharacterBaseState state)
    {
        if (_state is EnemiesGetHitState && state is EnemiesGetHitState
            || _state is EnemiesDieState)
            return;

        _state.ExitState();
        _state = state;
        _state.EnterState(this);
    }

    protected virtual void HandleChangeDirection()
    {
        if (_hasDetectedWall || !_hasDetectedGround && _groundCheck)
            FlippingSprite();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag(GameConstants.PLAYER_SWORD_TAG) && !_hasGetHit)
        {
            _hasGetHit = true;
            SpawnBloodVfx(collision.ClosestPoint(transform.position));
            ChangeState(_getHitState);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstants.PLAYER_SWORD_TAG) && !_hasGetHit)
        {
            _hasGetHit = true;
            SpawnBloodVfx(collision.ClosestPoint(transform.position));
            ChangeState(_getHitState);
        }
    }

    private void SpawnBloodVfx(Vector3 pos)
    {
        GameObject bloodVfx = PoolManager.Instance.GetObjectInPool(EPoolable.BloodVfx);
        bloodVfx.SetActive(true);
        bloodVfx.transform.position = pos;
    }

    private void ReceiveDamage(object obj)
    {
        DamageReceiveInfor dmgReceiveInfo = (DamageReceiveInfor)obj;
        if (_enemyID != dmgReceiveInfo.ID) return;

        _currentHP -= dmgReceiveInfo.DamageReceive;
        _currentHP = Mathf.Clamp(_currentHP, 0f, _enemiesSO.BaseHP);
        _hpFill.DOFillAmount(_currentHP / _enemiesSO.BaseHP, _enemiesSO.FillSpeed);
        if (_currentHP == 0) ChangeState(_dieState);
    }

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
            _detectPlayerRayFront = Physics2D.Raycast(_playerCheck.position, Vector2.left, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);
            _detectPlayerRayBack = Physics2D.Raycast(_playerCheck2.position, Vector2.right, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);
        }
        else
        {
            _detectPlayerRayFront = Physics2D.Raycast(_playerCheck.position, Vector2.right, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);
            _detectPlayerRayBack = Physics2D.Raycast(_playerCheck2.position, Vector2.left, _enemiesSO.PlayerCheckDistance, _enemiesSO.PlayerLayer);
        }

        if (_detectPlayerRayFront && _detectPlayerRayFront.collider.CompareTag(GameConstants.PLAYER_TAG))
            _hasDetectedPlayer = true;
        else
            _hasDetectedPlayer = false;

        if (_detectPlayerRayBack && _detectPlayerRayBack.collider.CompareTag(GameConstants.PLAYER_TAG))
            _hasDetectedPlayerBackward = true;
        else
            _hasDetectedPlayerBackward = false;
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

        if (_hasDetectedPlayerBackward)
        {
            if (!_isFacingRight)
                Debug.DrawRay(_playerCheck2.position, Vector2.right * _enemiesSO.PlayerCheckDistance, Color.yellow);
            else
                Debug.DrawRay(_playerCheck2.position, Vector2.left * _enemiesSO.PlayerCheckDistance, Color.yellow);
        }
        else
        {
            if (!_isFacingRight)
                Debug.DrawRay(_playerCheck2.position, Vector2.right * _enemiesSO.PlayerCheckDistance, Color.blue);
            else
                Debug.DrawRay(_playerCheck2.position, Vector2.left * _enemiesSO.PlayerCheckDistance, Color.blue);
        }
    }

    protected virtual bool DetectWall()
    {
        if (!_isFacingRight)
            _hasDetectedWall = Physics2D.Raycast(_wallCheck.position, Vector2.left, _enemiesSO.GWCheckDistance, _enemiesSO.GWLayer);
        else
            _hasDetectedWall = Physics2D.Raycast(_wallCheck.position, Vector2.right, _enemiesSO.GWCheckDistance, _enemiesSO.GWLayer);

        return _hasDetectedWall;
    }

    protected virtual void DrawRayDetectWall()
    {
        if (!_hasDetectedGround)
        {
            if (!_isFacingRight)
                Debug.DrawRay(_wallCheck.position, Vector2.left * _enemiesSO.GWCheckDistance, Color.green);
            else
                Debug.DrawRay(_wallCheck.position, Vector2.right * _enemiesSO.GWCheckDistance, Color.green);
        }
        else
        {
            if (!_isFacingRight)
                Debug.DrawRay(_wallCheck.position, Vector2.left * _enemiesSO.GWCheckDistance, Color.red);
            else
                Debug.DrawRay(_wallCheck.position, Vector2.right * _enemiesSO.GWCheckDistance, Color.red);
        }
    }

    protected virtual void DetectGround()
    {
        if (_groundCheck)
            _hasDetectedGround = Physics2D.Raycast(_groundCheck.position, Vector2.down, _enemiesSO.GWCheckDistance, _enemiesSO.GWLayer);
    }

    protected virtual void DrawRayDetectGround()
    {
        if (_groundCheck)
            if (!_hasDetectedGround)
                Debug.DrawRay(_groundCheck.position, Vector2.down * _enemiesSO.GWCheckDistance, Color.green);
            else
                Debug.DrawRay(_groundCheck.position, Vector2.down * _enemiesSO.GWCheckDistance, Color.red);
    }

    protected virtual void SelfDestroy() { Destroy(gameObject); }

    public void DeactiveHPUI()
    {
        _hpBar.SetActive(false);
    }

    public IEnumerator TriggerAttack()
    {
        yield return new WaitForSeconds(_enemiesSO.AttackDelay);

        //Sau khoảng delay ngắn, nếu vẫn phát hiện Player thì attack, 0 thì về Idle
        ChangeState((_hasDetectedPlayer) ? _attackState : _idleState);
    }

    public IEnumerator DelayFlip()
    {
        yield return new WaitForSeconds(_enemiesSO.DelayFlipIfPlayerBackward);

        FlippingSprite();

        yield return new WaitForSeconds(0.2f);
        _idleState.HasStartedFlip = false;
    }

    public override void FlippingSprite()
    {
        if (_state is EnemiesDieState) return;

        base.FlippingSprite();
    }

    //Anim event của Atk
    protected void BackToIdle()
    {
        ChangeState(_idleState);
    }

    //Khi attack thì WakeUp rb để bắt va chạm
    protected void WakeUpRigidBody2D()
    {
        _rb.WakeUp();
    }

    //1 số enemy sẽ có đòn attack thứ 2
    protected void ChangeToAttack2()
    {
        ChangeState(_attack2State);
    }
}
