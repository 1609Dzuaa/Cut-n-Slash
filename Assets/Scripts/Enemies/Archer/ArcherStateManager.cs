using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ArcherStateManager : EnemiesStateManager
{
    //Fix bug Archer

    [Header("Withdrawn Force")]
    [SerializeField] Vector2 _withdrawnForce;

    [Header("Range"), Tooltip("Khoảng cách mà khi Player ở đủ gần" +
        "thì nó sẽ Withdrawn")]
    [SerializeField] Vector2 _withdrawnableRange;

    [SerializeField] Transform _shootPos;

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
    }

    //event của animation Attack
    private void SpawnArrow()
    {
        GameObject archerArrow = PoolManager.Instance.GetObjectInPool(EPoolable.ArcherArrow);
        archerArrow.SetActive(true);
        archerArrow.transform.position = _shootPos.position;

        string id = archerArrow.GetComponent<ArrowController>().ID;
        ArrowInfor info = new(id, _shootPos.position, _isFacingRight);
        EventsManager.Instance.NotifyObservers(EEvents.ArrowOnReceiveInfor, info);
    }

}
