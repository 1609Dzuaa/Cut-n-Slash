using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;
using static GameConstants;
using static UnityEditor.PlayerSettings;

public class PlayerStateManager : CharactersStateManager
{
    //Bug animation bị đứng ở GetHit khi bị nhiều enemy tđvl

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
    [SerializeField] float _dashSpeed;
    [SerializeField] float _gravScale;
    [SerializeField] float _rollSpeed;
    [SerializeField, Tooltip("Bơm cho Player " +
        "vận tốc nhỏ khi Attack")] float _moveSpeedWhileAttack;

    [Header("Time")]
    [SerializeField, Tooltip("Khoảng thời gian" +
        "trở về Idle sau khi không click combo tiếp")] 
    float _delayBackToIdle;
    [SerializeField, Tooltip("Khoảng thời gian" +
        "ngắn cho phép Player thi triển combo tiếp theo")]
    float _enableComboTime;
    [SerializeField, Tooltip("Khoảng thgian delay Update" +
        " khi đang ở AttackState")]
    //Mục đích để ngăn chuyển state để chạy hết anim tránh bị loạn
    //Ý tưởng Attack là: Atk1->Idle->Atk2->Idle->Atk3->Idle
    //Với khoảng thgian Idle là rất nhỏ
    float _delayUpdateAttack; //thừa, xem xét bỏ ?
    [SerializeField, Tooltip("Khoảng thgian nếu Fall quá lâu" +
        "sẽ chuyển sang Land thay vì Idle như bthg")]
    float _timeCanLanding;
    [SerializeField] float _delayUpdateDash; //Tương tự như trên
    [SerializeField] float _dashDelay;
    [SerializeField] float _rollDelay;
    [SerializeField] float _inVulnerableTime;

    [Header("Get Hit Related")]
    [SerializeField] float _alphaValGetHit;
    [SerializeField] float _timeEachApplyAlpha;

    #region Player's States

    PlayerIdleState _idleState = new();
    PlayerWalkState _walkState = new();
    PlayerRunState _runState = new();
    PlayerJumpState _jumpState = new();
    PlayerFallState _fallState = new();
    PlayerAttack1State _attack1State = new();
    PlayerAttack2State _attack2State = new();
    PlayerAttack3State _attack3State = new();
    PlayerDashState _dashState = new();
    PlayerRollState _rollState = new();
    PlayerLandingState _landingState = new();
    PlayerGetHitState _getHitState = new();

    #endregion

    #region Player's Attributes

    bool _isOnPlatform;
    bool _isOnGround;
    bool _hasGetHit; //Tránh Trigger bị gọi nhiều lần
    bool _canJump;
    bool _isWallTouch;
    bool _hasStartCoroutine;
    bool _isApplyGetHitEffect;
    RaycastHit2D _isWallHit;
    SpriteRenderer _spriteRenderer;
    float _dirX, _dirY;

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

    public PlayerDashState DashState { get => _dashState; set => _dashState = value; }

    public PlayerRollState RollState { get => _rollState; set => _rollState = value; }

    public PlayerLandingState LandingState { get => _landingState; set => _landingState = value; }

    #endregion

    #region Public Field Properties

    public float WalkSpeed { get => _walkSpeed; }

    public float RunSpeed { get => _runSpeed; }

    public bool IsOnGround { get => _isOnGround; }

    public float DirX { get => _dirX; }

    public float DirY { get => _dirY; }

    public float JumpForce { get => _jumpForce; }

    public float DashSpeed { get => _dashSpeed; }

    public float GravScale { get => _gravScale; }

    public float EnableComboTime { get => _enableComboTime; }

    public bool IsApplyGetHitEffect { get => _isApplyGetHitEffect; set => _isApplyGetHitEffect = value; }

    public float DelayUpdateDash { get=> _delayUpdateDash; }

    public float DashDelay { get => _dashDelay; }

    public float RollDelay { get => _rollDelay; }

    public float TimeCanLanding { get => _timeCanLanding; }

    public float MoveSpeedWhileAttack { get => _moveSpeedWhileAttack; }

    public float RollSpeed { get => _rollSpeed; }

    public bool HasGetHit { get => _hasGetHit; set => _hasGetHit = value; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void GetReferenceComponents()
    {
        base.GetReferenceComponents();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    protected override void Update()
    {
        base.Update();
        HandleInput();
        HandleFlipSprite();
        HandleAlphaValueGetHit();
        //Debug.Log("hGH: " + _hasGetHit);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        GroundAndWallCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TRAP_TAG))
            ChangeState(_getHitState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMIES_WEAPON_TAG) || collision.CompareTag(TRAP_TAG) && !_hasGetHit)
        {
            if (Time.time - _getHitState.EntryTime < _inVulnerableTime) return;

            _hasGetHit = true;
            SpawnBloodVfx(collision.ClosestPoint(transform.position));
            ChangeState(_getHitState);
        }
        //Debug.Log("here: " + _hasGetHit);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMIES_WEAPON_TAG) || collision.CompareTag(TRAP_TAG) && !_hasGetHit)
        {
            if (Time.time - _getHitState.EntryTime < _inVulnerableTime) return;

            _hasGetHit = true;
            SpawnBloodVfx(collision.ClosestPoint(transform.position));
            ChangeState(_getHitState);
        }
        //Debug.Log("hereStay: " + _hasGetHit);
    }

    private void SpawnBloodVfx(Vector3 pos)
    {
        GameObject bloodVfx = PoolManager.Instance.GetObjectInPool(EPoolable.BloodVfx);
        bloodVfx.SetActive(true);
        bloodVfx.transform.position = pos;
    }

    private void HandleInput()
    {
        _dirX = Input.GetAxisRaw(HORIZONTAL_AXIS);
        _dirY = Input.GetAxisRaw(VERTICAL_AXIS);
    }

    private void HandleFlipSprite()
    {
        if (_state is PlayerRollState || _state is PlayerDashState) return;

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

    //Start Coroutine khi ở đang ở Attack1 || Attack2
    //Để trở về state Idle khi 0 tiếp tục combo
    public IEnumerator BackToIdle()
    {
        yield return new WaitForSeconds(_delayBackToIdle);

        ChangeState(_idleState);
    }

    //Event của Animation Attack 3
    //Đặt ở cuối Frame
    private void AnimEventBackToIdle()
    {
        ChangeState(_idleState);
    }

    /// <summary>
    ///2 hàm dưới để xử lý màu alpha khi player dính đòn  
    /// </summary>

    private IEnumerator Twinkling()
    {
        //Lock - Đảm bảo chỉ gọi coroutine sau khi đi đc 1 vòng Alpha Value:
        //từ 1 -> AlphaVal -> 1
        _hasStartCoroutine = true;
        _spriteRenderer.color = new Color(1f, 1f, 1f, _alphaValGetHit);
        //Debug.Log("tang lan: " + _count);

        yield return new WaitForSeconds(_timeEachApplyAlpha);

        //Thêm check đây nữa 
        /*if (BuffsManager.Instance.GetTypeOfBuff(EBuffs.Invisible).IsAllowToUpdate)
        {
            _hasStartCoroutine = false;
            yield return null;
        }
        else*/
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(_timeEachApplyAlpha);

        _hasStartCoroutine = false;
    }

    private void HandleAlphaValueGetHit()
    {
        //if (BuffsManager.Instance.GetTypeOfBuff(EBuffs.Invisible).IsAllowToUpdate)
            //return;

        if (Time.time - _getHitState.EntryTime <= _inVulnerableTime && !_hasStartCoroutine && _isApplyGetHitEffect)
            StartCoroutine(Twinkling());
        else if (Time.time - _getHitState.EntryTime > _inVulnerableTime)
        {
            //Hết thgian miễn dmg r thì trả màu về như cũ cho nó
            //NẾU trên ng 0 có buff vô hình, còn có thì return và set lại bool
            /*if (BuffsManager.Instance.GetTypeOfBuff(EBuffs.Invisible).IsAllowToUpdate)
            {
                _isApplyGetHitEffect = false;
                return;
            }*/

            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _isApplyGetHitEffect = false;
        }
    }

}
