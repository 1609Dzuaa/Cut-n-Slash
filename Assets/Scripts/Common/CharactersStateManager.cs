using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersStateManager : GameObjectController
{
    protected CharacterBaseState _state;
    protected float _hp;
    protected float _damageDealt;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void GetReferenceComponents()
    {
        base.GetReferenceComponents();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update() { _state?.UpdateState(); }

    protected virtual void FixedUpdate() { _state?.FixedUpdate(); }

    public virtual void ChangeState(CharacterBaseState state)
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
