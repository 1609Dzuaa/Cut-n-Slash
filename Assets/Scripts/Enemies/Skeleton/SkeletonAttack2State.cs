using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack2State : EnemiesAttackState
{
    //Vấn đề Trigger kh hoạt động khi bật/tắt trong animation:
    //https://forum.unity.com/threads/reenabling-disabled-gameobject-does-not-call-ontriggerenter.765551/?fbclid=IwAR3uxpqf0r7nQAslGms80rx-jFIRo33LKxzOXsluCGrnWMASv2pUkIoi_NE_aem_AQPcUuR6j0jxfS4Zjq4GQedMNPpdWxURCVhVzcD2N-GGrS5avPFO4KrSYtZA1wFPKFLPy7VndpXJG1mSo2IpiFgR
    //https://community.gamedev.tv/t/ontriggerenter2d-not-detecting-hits-consistently/224422/9
    //Solution khả dĩ:
    //lúc enable childgameobj thì wakeup rb
    //add rb cho cả childgameobj

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.ESkeletonState.Attack2);
        Debug.Log("Skl Atk2");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
