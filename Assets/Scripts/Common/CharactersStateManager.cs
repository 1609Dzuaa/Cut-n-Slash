using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersStateManager : GameObjectController
{
    protected CharacterBaseState _state;
    protected Rigidbody2D _rb;
    protected bool _isFacingRight = true;
    protected float _hp;
    protected float _damageDealt;

    public Rigidbody2D GetRigidbody2D { get => _rb; set => _rb = value; }

    public bool IsFacingRight { get => _isFacingRight; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void GetReferenceComponents()
    {
        base.GetReferenceComponents();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update() { if (_state != null) _state.UpdateState(); }

    protected virtual void FixedUpdate() { if (_state != null) _state.FixedUpdate(); }

    public void ChangeState(CharacterBaseState state)
    {
        _state.ExitState();
        _state = state;
        _state.EnterState(this);
    }

    public void FlippingSprite()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}
