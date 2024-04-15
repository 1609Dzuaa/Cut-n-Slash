using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ArcherStateManager : EnemiesStateManager
{
    //Fix bug Archer
    //Thêm state teleport (liệt kê các TH bị ép kh withdrawn đc)

    [Header("Withdrawn Related")]
    [SerializeField] Vector2 _withdrawnForce;
    [Tooltip("Khoảng cách mà khi Player ở đủ gần" +
        " thì nó sẽ Withdrawn")]
    [SerializeField] Vector2 _withdrawnableRange;
    [SerializeField, Tooltip("Khoảng thgian để có thể Withdrawn tiếp")] float _withdrawnDelay;

    [SerializeField] Transform _shootPos;

    [Header("Teleport Related"), Tooltip("Archer sẽ Tele nếu bị Player dồn vào góc chết:" +
        " Phía sau có tường hoặc bờ vực")]
    [SerializeField] Transform _groundCheck2;
    [SerializeField] Transform _wallCheck2;
    [SerializeField] Vector2 _check2Size;
    [SerializeField] float _teleDist;

    #region States

    ArcherIdleState _archerIdleState = new();
    ArcherPatrolState _archerPatrolState = new();
    ArcherWithdrawnState _archerWithdrawnState = new();
    ArcherTeleportState _archerTeleportState = new();

    #endregion

    Collider2D _playerCol;
    bool _isWallBehind;
    bool _isGroundBehind;

    public ArcherIdleState GetArcherIdleState() { return _archerIdleState; }

    public ArcherWithdrawnState GetArcherWithdrawnState() { return _archerWithdrawnState; }

    public ArcherTeleportState GetArcherTeleportState() { return _archerTeleportState; }

    public Vector2 WithdrawnForce { get => _withdrawnForce; }

    public float WithdrawnDelay { get => _withdrawnDelay; }

    public float TeleportDist { get => _teleDist; }

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
        _playerCol = Physics2D.OverlapBox(transform.position, _withdrawnableRange, _enemiesSO.PlayerLayer);
        return _playerCol.CompareTag(GameConstants.PLAYER_TAG);
    }

    public bool TeleportCheck()
    {
        _isWallBehind = Physics2D.OverlapBox(_wallCheck2.position, _check2Size, 0f, _enemiesSO.GWLayer);
        _isGroundBehind = Physics2D.OverlapBox(_groundCheck2.position, _check2Size, 0f, _enemiesSO.GWLayer);

        return _isWallBehind || !_isWallBehind && !_isGroundBehind;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, _withdrawnableRange);
        Gizmos.DrawCube(_groundCheck2.position, _check2Size);
        Gizmos.DrawCube(_wallCheck2.position, _check2Size);
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
