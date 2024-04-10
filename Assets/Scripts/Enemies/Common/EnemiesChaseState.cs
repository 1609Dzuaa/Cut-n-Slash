using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesChaseState : EnemiesBaseState
{
    //State này sẽ đuổi theo Player cho đến khi đạt ngưỡng AttackableRange
    //thì switch sang Attack (Áp dụng cho cả Melee, Ranged Enemies)
    bool _hasTriggeredAttack;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EEnemiesCommonState.Chase);
        Debug.Log("Enemies Base Chase");
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
        else
        {
            Vector2 currentPos = _enemiesSM.transform.position;
            Vector2 playerPos = _enemiesSM.PlayerRef.position;
            float chaseSpeed = _enemiesSM.GetEnemiesSO().ChaseSpeed;

            _enemiesSM.transform.position = Vector2.MoveTowards(currentPos, playerPos, chaseSpeed * Time.deltaTime);
        } 
            
    }

    protected virtual bool CheckIfCanAttack()
    {
        Vector2 currentPos = _enemiesSM.transform.position;
        Vector2 playerPos = _enemiesSM.PlayerRef.position;
        float attackableDist = _enemiesSM.GetEnemiesSO().AttackableDistance;

        return _enemiesSM.HasDetectedPlayer && !_hasTriggeredAttack
            && Vector2.Distance(currentPos, playerPos) <= attackableDist;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}