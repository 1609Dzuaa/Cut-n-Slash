using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesChaseState : EnemiesBaseState
{
    //State này sẽ đuổi theo Player cho đến khi đạt ngưỡng AttackableRange
    //thì switch sang Attack (Áp dụng cho cả Melee, Ranged Enemies)
    //Bug rượt mà drag player theo và rượt ngược hướng Player ?
    bool _hasTriggeredAttack;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Chase);
        _enemiesSM.GetRigidbody2D.velocity = Vector2.zero;
        //Debug.Log("Enemies Base Chase");
    }

    public override void ExitState()
    {
        _hasTriggeredAttack = false;
    }

    public override void UpdateState()
    {
        if (CheckIfCanAttack())
        {
            _hasTriggeredAttack = true;
            _enemiesSM.StartCoroutine(_enemiesSM.TriggerAttack());
        }
        else if (CheckIfCanIdle())
            _enemiesSM.ChangeState(_enemiesSM.GetIdleState());
        else
            HandleFlipTowardsPlayer();
    }

    protected virtual bool CheckIfCanAttack()
    {
        Vector2 currentPos = _enemiesSM.transform.position;
        Vector2 playerPos = _enemiesSM.PlayerRef.position;
        float attackableDist = _enemiesSM.GetEnemiesSO().AttackableDistance;

        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack
            && Vector2.Distance(currentPos, playerPos) <= attackableDist;
    }

    protected virtual bool CheckIfCanIdle()
    {
        return !_enemiesSM.HasDetectedPlayer;
    }

    protected void HandleFlipTowardsPlayer()
    {
        float playerPosX = _enemiesSM.PlayerRef.transform.position.x;
        float currentPosX = _enemiesSM.transform.position.x;
        bool isRight = _enemiesSM.IsFacingRight;

        if (currentPosX > playerPosX + GameConstants.FLIPABLE_OFFSET && isRight)
            _enemiesSM.FlippingSprite();
        else if (currentPosX < playerPosX - GameConstants.FLIPABLE_OFFSET && !isRight)
            _enemiesSM.FlippingSprite();
    }

    public override void FixedUpdate()
    {
        //Move = velo thay vì MoveTowards cho đỡ bug
        float chaseSpeed = _enemiesSM.GetEnemiesSO().ChaseSpeed;
        float yVelo = _enemiesSM.GetRigidbody2D.velocity.y;
        bool isRight = _enemiesSM.IsFacingRight;

        _enemiesSM.GetRigidbody2D.velocity = new((isRight) ? chaseSpeed : -chaseSpeed, yVelo);
    }
}