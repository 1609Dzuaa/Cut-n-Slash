using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesStateManager : CharactersStateManager
{
    [Header("Check Properties")]
    [SerializeField] protected Transform _playerCheck;
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected Transform _wallCheck;

    [Header("SO Data")]
    [SerializeField] protected EnemiesSO _enemiesSO;

    #region States
    protected EnemiesIdleState _idleState = new();
    protected EnemiesPatrolState _patrolState = new();
    protected EnemiesAttackState _attackState = new();
    protected EnemiesGetHitState _getHitState = new();
    protected EnemiesDieState _dieState = new();
    #endregion

    protected RaycastHit2D _pRayHit;
    protected bool _hasDetectedPlayer;
    protected bool _hasDetectedGround;
    protected bool _hasDetectedWall;

    public EnemiesSO GetEnemiesSO() { return _enemiesSO; }

    public EnemiesIdleState GetIdleState() { return _idleState; }

    public EnemiesPatrolState GetPatrolState() { return _patrolState; }

    public EnemiesAttackState GetAttackState() { return _attackState; }

    public EnemiesGetHitState GetGetHitState() { return _getHitState; }

    public EnemiesDieState GetDieState() { return _dieState; }

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
        _state = _idleState;
        _state.EnterState(this);
        //Debug.Log("IfR, yAngles: " + _isFacingRight + ", " + transform.rotation.eulerAngles.y);
    }

    protected override void Update()
    {
        base.Update();
        DetectPlayer();
        DrawRayDetectPlayer();
    }

    public override void ChangeState(CharacterBaseState state)
    {
        if (_state is EnemiesGetHitState && state is EnemiesGetHitState)
            return;

        _state.ExitState();
        _state = state;
        _state.EnterState(this);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag(GameConstants.PLAYER_SWORD_TAG))
        {
            ChangeState(_getHitState);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstants.PLAYER_SWORD_TAG))
        {
            ChangeState(_getHitState);
        }
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

    public IEnumerator TriggerAttack()
    {
        yield return new WaitForSeconds(_enemiesSO.AttackDelay);

        ChangeState(_attackState);
    }

    //Anim event của Atk
    protected void BackToIdle()
    {
        ChangeState(_idleState);
    }

}
