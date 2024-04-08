using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStateManager : EnemiesStateManager
{
    [Header("Withdrawn Force")]
    [SerializeField] Vector2 _withdrawnForce;

    [Header("Range"), Tooltip("Khoảng cách mà khi Player ở đủ gần" +
        "thì nó sẽ Withdrawn")]
    [SerializeField] Vector2 _withdrawnableRange;

    #region States

    ArcherIdleState _archerIdleState = new();
    ArcherPatrolState _archerPatrolState = new();
    ArcherAttackState _archerAttackState = new();
    ArcherWithdrawnState _archerWithdrawnState = new();

    #endregion

    Collider2D _playerCol;

    public ArcherIdleState GetArcherIdleState() { return _archerIdleState; }

    public ArcherWithdrawnState GetArcherWithdrawnState() { return _archerWithdrawnState; }

    public Vector2 WithdrawnForce { get => _withdrawnForce; }

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
        _idleState = _archerIdleState;
        _patrolState = _archerPatrolState;
        _attackState = _archerAttackState;
        base.SetupProperties();
    }

    protected override void Update()
    {
        base.Update();
        WithdrawnCheck();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public bool WithdrawnCheck()
    {
        _playerCol = Physics2D.OverlapBox(transform.position, _withdrawnableRange, _enemiesSO.PlayerLayer);
        return _playerCol.CompareTag(GameConstants.PLAYER_TAG);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, _withdrawnableRange);
        //Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _withdrawnableRange, transform.position.y, transform.position.z));
        //Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - _withdrawnableRange, transform.position.y, transform.position.z));
    }

}
