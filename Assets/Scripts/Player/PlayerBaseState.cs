using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : CharacterBaseState
{
    protected PlayerStateManager _playerSM;

    //Các state Idle, Move, Jump, ... của Player chỉ cần kế thừa từ thg base
    //r gọi base.Enter ở Enter của nó là có thể sử dụng pSM lấy data

    public override void EnterState(CharactersStateManager charactersSM)
    {
        _playerSM = (PlayerStateManager)charactersSM;
        //Debug.Log("init pSM");
    }
}
