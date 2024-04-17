using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ArcherStateManager : EnemiesStateManager
{
    [Header("SO")]
    [SerializeField] ArcherSO _archerSO;

    [SerializeField] Transform _shootPos;

    [Header("Teleport Related"), Tooltip("Archer sẽ Tele nếu bị Player dồn vào góc chết:" +
        " Phía sau có tường hoặc bờ vực")]
    [SerializeField] Transform _groundCheck2;
    [SerializeField] Transform _wallCheck2;

    #region States

    ArcherIdleState _archerIdleState = new();
    ArcherPatrolState _archerPatrolState = new();
    ArcherWithdrawnState _archerWithdrawnState = new();
    ArcherTeleportState _archerTeleportState = new();

    #endregion

    Collider2D _playerCol;
    bool _isWallBehind;
    bool _isGroundBehind;

    public ArcherSO ArcherSO { get => _archerSO; } 

    public ArcherWithdrawnState GetArcherWithdrawnState() { return _archerWithdrawnState; }

    public ArcherTeleportState GetArcherTeleportState() { return _archerTeleportState; }

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
        _enemiesSO = _archerSO;
        _idleState = _archerIdleState;
        _patrolState = _archerPatrolState;
        base.SetupProperties();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        WithdrawnCheck();
        TeleportCheck();
    }

    protected override void HandleChangeDirection()
    {
        if (_state is ArcherWithdrawnState || _state is EnemiesAttackState) return;

        base.HandleChangeDirection();
    }

    public bool WithdrawnCheck()
    {
        _playerCol = Physics2D.OverlapBox(transform.position, _archerSO.WithdrawnableRange, _enemiesSO.PlayerLayer);
        if (_playerCol == null) return false;
        return _playerCol.CompareTag(GameConstants.PLAYER_TAG);
    }

    public bool TeleportCheck()
    {
        _isWallBehind = Physics2D.OverlapBox(_wallCheck2.position, _archerSO.Check2Size, 0f, _enemiesSO.GWLayer);
        _isGroundBehind = Physics2D.OverlapBox(_groundCheck2.position, _archerSO.Check2Size, 0f, _enemiesSO.GWLayer);

        return _isWallBehind || !_isWallBehind && !_isGroundBehind;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, _archerSO.WithdrawnableRange);
        Gizmos.DrawCube(_groundCheck2.position, _archerSO.Check2Size);
        Gizmos.DrawCube(_wallCheck2.position, _archerSO.Check2Size);
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
