using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : CharactersStateManager
{
    [Header("Check Properties")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] Transform _wallCheck;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] LayerMask _gwLayer;

    [Header("Speed, Force, Grav Properties")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _gravScale;

    #region Player's States

    PlayerIdleState _idleState = new();
    PlayerWalkState _walkState = new();
    PlayerRunState _runState = new();
    PlayerJumpState _jumpState = new();
    PlayerFallState _fallState = new();
    PlayerAttack1State _attack1State = new();
    PlayerAttack2State _attack2State = new();
    PlayerAttack3State _attack3State = new();

    #endregion

    #region Player's Attributes

    bool _isOnPlatform;
    bool _isOnGround;
    bool _canJump;
    bool _isWallTouch;
    RaycastHit2D _isWallHit;
    float _dirX;

    #endregion

    #region Public Field States

    public PlayerIdleState IdleState { get => _idleState; set => _idleState = value; }

    public PlayerWalkState WalkState { get => _walkState; set => _walkState = value; }

    public PlayerRunState RunState { get => _runState; set => _runState = value; }

    public PlayerJumpState JumpState { get => _jumpState; set => _jumpState = value; }

    public PlayerFallState FallState { get => _fallState; set => _fallState = value; }
    
    public PlayerAttack1State Attack1State { get => _attack1State; set => _attack1State = value; }

    public PlayerAttack2State Attack2State { get => _attack2State; set => _attack2State = value; }

    public PlayerAttack3State Attack3State { get => _attack3State; set => _attack3State = value; }

    #endregion

    #region Public Field Properties

    public float WalkSpeed { get => _walkSpeed; }

    public float RunSpeed { get => _runSpeed; }

    public bool IsOnGround { get => _isOnGround; }

    public float DirX { get => _dirX; }

    public float JumpForce { get => _jumpForce; }

    public float GravScale { get => _gravScale; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void GetReferenceComponents()
    {
        base.GetReferenceComponents();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetupProperties()
    {
        _state = _idleState;
        _state.EnterState(this);
    }

    protected override void Update()
    {
        base.Update();
        HandleInput();
        HandleFlipSprite();
        GroundAndWallCheck();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void HandleInput()
    {
        //if (_hasWinGame) return;
        _dirX = Input.GetAxisRaw(GameConstants.HORIZONTAL_AXIS);
    }

    private void HandleFlipSprite()
    {
        if (_dirX > 0f && !_isFacingRight)
            FlippingSprite();
        else if (_dirX < 0f && _isFacingRight)
            FlippingSprite();
    }

    private void GroundAndWallCheck()
    {
        //Ở trên Platform thì 0 cần GCheck tránh bug
        if (!_isOnPlatform)
            _canJump = _isOnGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _gwLayer);
        else
            _canJump = true; //done bug
        if (_isFacingRight)
        {
            _isWallTouch = Physics2D.Raycast(_wallCheck.position, Vector2.right, _wallCheckDistance, _gwLayer);
            _isWallHit = Physics2D.Raycast(_wallCheck.position, Vector2.right, _wallCheckDistance, _gwLayer);
        }
        else
        {
            _isWallTouch = Physics2D.Raycast(_wallCheck.position, Vector2.left, _wallCheckDistance, _gwLayer);
            _isWallHit = Physics2D.Raycast(_wallCheck.position, Vector2.left, _wallCheckDistance, _gwLayer);
        }
    }

    private void OnDrawGizmos()
    {
        //Draw Ground Check
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);

        //Draw Wall Check
        if (_isFacingRight)
            Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y, _wallCheck.position.z));
        else
            Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x - _wallCheckDistance, _wallCheck.position.y, _wallCheck.position.z));
    }
}
